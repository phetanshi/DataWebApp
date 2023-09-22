using Microsoft.OpenApi.Models;

namespace DataWebApp.Api.AppStart
{
    public static class MiscAppServices
    {
        public static void AddSwaggerWithAutherization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerWithAzureAd(configuration);
        }

        private static void AddSwaggerWithAzureAd(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DataWebApp", Version = "v1" });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "OAuth2.0 Auth Code with PKCE",
                    Name = "oauth2",
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(configuration["SwaggerAzureAd:AuthorizationUrl"]),
                            TokenUrl = new Uri(configuration["SwaggerAzureAd:TokenUrl"]),
                            Scopes = new Dictionary<string, string>
                            {
                                {configuration["SwaggerAzureAd:Scope"], "read the api" }
                            }
                        }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "oauth2"
                              }
                          },
                         new[] { configuration["SwaggerAzureAd:Scope"] }
                    }
                });

            });
        }

    }
}
