using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace DataWebApp.Api.AppStart
{
    public static class AuthenticationService
    {
        public static IServiceCollection AddAppAuthenticationSchemes(this IServiceCollection services, IConfiguration config)
        {
            services.AddAzureAd(config);
            return services;
        }

        private static void AddAzureAd(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(config.GetSection("AzureAd"));
        }
    }
}
