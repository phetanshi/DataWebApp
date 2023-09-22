using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace DataWebApp.Logging
{
    public class DbLoggerConfiguration
    {
        public int EventId { get; set; }
        public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information;
        public string ConnectionString { get; set; }
        public string LoginUserId { get; set; }
        public IHttpContextAccessor AppContext { get; set; }
        public string GetLoginUserId()
        {
            if (this.AppContext != null)
            {
                return AppContext.HttpContext.User.Identity.Name ?? string.Empty;
            }
            else
                return string.Empty;
        }

        public string GetDisplayUrl()
        {
            if (this.AppContext != null)
            {
                return AppContext.HttpContext.Request.GetDisplayUrl() ?? string.Empty;
            }
            else
                return string.Empty;
        }
    }
}