using DataWebApp.Logging.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DataWebApp.Logging.DependencyInjection
{
    public static class LoggerService
    {
        public static void AddAppLogger(this ILoggingBuilder loggingBuilder, IServiceCollection services, IConfiguration config)
        {
            string logConnStr = config.GetConnectionString("AppLogDbConnection");
            string minimulLogLevel = config["Logging:LogLevel:DataWebApp.Logging.DbLogger"] ?? config["Logging:LogLevel:Default"] ?? "Information";
            loggingBuilder.AddDbLogger(config =>
            {
                config.ConnectionString = logConnStr;
                Enum.TryParse(minimulLogLevel, true, out LogLevel configLoglevel);
                config.MinimumLogLevel = configLoglevel;
            });

            services.AddScoped<DbLoggerProvider>();
        }
    }
}
