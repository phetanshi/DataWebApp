using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DataWebApp.Logging
{
    [ProviderAlias("DataWebApp.Logging.DbLogger")]
    public class DbLoggerProvider : ILoggerProvider
    {
        private readonly IDisposable _onChangeToken;
        private readonly IConfiguration _config;
        private DbLoggerConfiguration _dbLoggerConfig;

        public DbLoggerProvider(IOptionsMonitor<DbLoggerConfiguration> logConfigOptions, IConfiguration config, IHttpContextAccessor httpContextAccess)
        {
            this._dbLoggerConfig = logConfigOptions.CurrentValue;
            this._dbLoggerConfig.AppContext = httpContextAccess;
            this._onChangeToken = logConfigOptions.OnChange(updateConfig => _dbLoggerConfig = updateConfig);
            this._config = config;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(categoryName, _config, _dbLoggerConfig);
        }
        public void Dispose()
        {
            _onChangeToken.Dispose();
        }
    }
}
