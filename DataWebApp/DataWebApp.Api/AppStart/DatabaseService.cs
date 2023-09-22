using DataWebApp.Data.Database;
using Ps.EfCoreRepository.SqlServer.DependencyInjection;

namespace DataWebApp.Api.AppStart
{
    public static class DatabaseService
    {
        public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDatabase(builder.Configuration);
            return builder;
        }
    }
}
