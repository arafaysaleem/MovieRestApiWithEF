using Contracts;
using Entities;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using Repositories;
using System.Text;

namespace MovieRestApiWithEF.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services, string policyName)
        {
            // Add CORS service to pipeline
            services.AddCors(options =>
            {
                // Add a CORS policy with the specified name
                options.AddPolicy(policyName,
                    builder => builder
                    .AllowAnyOrigin() // allow from all locations
                    .AllowAnyMethod() // allow all HTTP verbs
                    .AllowAnyHeader()); // allow any kind of headers
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(_ => { });
        }

        public static void ConfigureDbContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            // Fetch connection string from app settings
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Inject Db service into app
            services.AddDbContext<MovieAppDbContext>(x =>
            {
                // MariaDB latest supported version
                var mySqlVersion = new Version(10, 8, 0);

                // Enable MySQL provider
                x.UseMySql(connectionString,
                    new MariaDbServerVersion(mySqlVersion),
                    b => b.MigrationsAssembly(nameof(Entities)) // Specify library that contains Migrations
                );
            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            // Get current directory
            var currDir = Directory.GetCurrentDirectory();

            // Add config path to current directory
            var configPath = string.Concat(currDir, "/nlog.config");

            // Load config for logger
            LogManager.LoadConfiguration(configPath);

            // Inject logger into app
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureJwtService(this IServiceCollection services, ConfigurationManager configuration)
        {
            var jwtTokenConfig = configuration.GetSection("jwt").Get<JwtTokenConfig>();
            services.AddSingleton(jwtTokenConfig);
            services.AddScoped<JwtService>();
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            // Inject an IoC repository container into app that manages access to all repositories
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void AddJwtAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            var jwtTokenConfig = configuration.GetSection("jwt").Get<JwtTokenConfig>();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    RequireSignedTokens = true,
                    RequireAudience = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = key
                };
            });
        }
    }
}
