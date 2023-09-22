using DataWebApp.Logging.Database;
using DataWebApp.Logging.Database.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DataWebApp.Logging
{
    public class DbLogger : ILogger
    {
        private readonly string name;
        private readonly DbLoggerConfiguration logConfig;
        private readonly AppLoggingDbContext loggerDbContext;

        public DbLogger(string name, IConfiguration configuration, DbLoggerConfiguration logConfig)
        {
            this.name = name;
            this.logConfig = logConfig;
            loggerDbContext = new AppLoggingDbContext(configuration);
        }
        public DbLogger(IConfiguration configuration, DbLoggerConfiguration logConfig)
        {
            this.logConfig = logConfig;
            loggerDbContext = new AppLoggingDbContext(configuration);
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return default;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logConfig.MinimumLogLevel <= logLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            try
            {
                string userId = logConfig.GetLoginUserId();
                string displayUrl = logConfig.GetDisplayUrl() ?? "-";

                if (!IsEnabled(logLevel))
                    return;

                string? message = formatter(state, exception);
                switch (logLevel)
                {
                    case LogLevel.Trace:
                    case LogLevel.Debug:
                    case LogLevel.Information:
                    case LogLevel.Warning:
                        ActivityLog logObj = GetActivityLogObject(message, userId, displayUrl, logLevel);
                        LogActivity(logObj);
                        break;
                    case LogLevel.Critical:
                    case LogLevel.Error:
                        ErrorLog error = GetErrorLogObject(message, userId, displayUrl, exception);
                        LogError(error);
                        break;
                }
            }
            catch { }
        }

        private TResult ReadObj<TState, TResult>(TState state)
        {
            try
            {
                var result = (TResult)Convert.ChangeType(state, typeof(TResult));
                return result;
            }
            catch { }
            return default(TResult);
        }

        private ActivityLog GetActivityLogObject(string message, string userId, string displayUrl, LogLevel logLevel)
        {
            ActivityLog logObj = new ActivityLog();
            logObj.UserId = userId;
            logObj.Message = message;
            logObj.UrlOrModule = displayUrl;
            logObj.LogLevelId = (int)logLevel;
            logObj.LogDateTime = DateTime.UtcNow;
            return logObj;
        }
        private ErrorLog GetErrorLogObject(string message, string userId, string displayUrl, Exception exception)
        {
            ErrorLog errorLog = new ErrorLog();
            errorLog.Message = message;
            errorLog.LogLevelId = (int)LogLevel.Error;
            errorLog.ErrorDateTime = DateTime.Now;
            errorLog.UrlOrModule = displayUrl;
            errorLog.UserId = userId;
            if (exception != null)
            {
                errorLog.ClassName = exception.TargetSite?.DeclaringType?.FullName;
                errorLog.MethodName = exception.TargetSite?.DeclaringType?.Name;
                errorLog.StackTrace = exception.StackTrace ?? "-";
                errorLog.ErrorType = exception.GetType()?.FullName ?? "-";
            }
            return errorLog;
        }

        private void LogActivity(ActivityLog activityLog)
        {
            try
            {
                loggerDbContext.ActivityLogs.Add(activityLog);
                loggerDbContext.SaveChanges();
            }
            catch { }
        }

        private void LogError(ErrorLog errorLog)
        {
            try
            {
                loggerDbContext.ErrorLogs.Add(errorLog);
                loggerDbContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        private void LogError(string message, Exception? exception)
        {
            try
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.Message = message;
                errorLog.LogLevelId = (int)LogLevel.Error;
                errorLog.ErrorDateTime = DateTime.Now;
                errorLog.UrlOrModule = "-";
                errorLog.UserId = "not-login";
                if (exception != null)
                {
                    errorLog.ClassName = exception.TargetSite?.DeclaringType?.FullName;
                    errorLog.MethodName = exception.TargetSite?.DeclaringType?.Name;
                    errorLog.StackTrace = exception.StackTrace ?? "-";
                    errorLog.ErrorType = exception.GetType()?.FullName ?? "-";
                }

                loggerDbContext.ErrorLogs.Add(errorLog);
                loggerDbContext.SaveChanges();

                if (exception?.InnerException != null)
                    LogError(exception.InnerException.Message, exception.InnerException);
            }
            catch { }
        }
    }
}
