using Contracts;
using Entities;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using NLog;
using Repository;

namespace MovieRestApiWithEF.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services, string policyName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName,
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(_ => { });
        }

        public static void ConfigureDbContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MovieAppDbContext>(x =>
            {
                var mySqlVersion = new Version(10, 4, 17);
                x.UseMySql(connectionString, new MySqlServerVersion(mySqlVersion));
            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            var currDir = Directory.GetCurrentDirectory();
            var configPath = string.Concat(currDir, "/nlog.config");
            LogManager.LoadConfiguration(configPath);

            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
    }
}
