using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ps.EfCoreRepository.SqlServer.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataWebApp.Data.Database
{
    public static class DatabaseServices
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEfCoreRepository<AppDbContext>(configuration, "AppDbConnection", true);
        }

        public static void SeedAppDatabase(this IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            var appDbContext = scope.ServiceProvider.GetService<AppDbContext>();
            if (appDbContext != null)
            {
                appDbContext.Database.EnsureCreated();
                appDbContext.Database.Migrate();
            }
        }
    }
}
