using DataWebApp.Api.Auth;
using DataWebApp.Api.AutoMapperProfiles;
using DataWebApp.Api.Services;
using DataWebApp.Api.Services.Definitions;
using Microsoft.AspNetCore.Authorization;

namespace DataWebApp.Api.AppStart
{
    public static class ApplicationObjects
    {
        public static IServiceCollection AddApplicationObjects(this IServiceCollection services)
        {
            services.AddServiceDependencies();
            services.AddOthes();
            return services;
        }

        private static void AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<ISampleService, SampleService>();
        }
        private static void AddOthes(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, AppAuthorizationHandler>();
            services.AddAutoMapper(typeof(EmployeeAutoMapperProfile));
        }
    }
}
