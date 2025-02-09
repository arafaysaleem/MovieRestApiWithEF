using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieRestApiWithEF.API.Filters;
using MovieRestApiWithEF.Application;
using MovieRestApiWithEF.Core;
using MovieRestApiWithEF.Core.Models;
using MovieRestApiWithEF.Infrastructure;
using NLog;
using System.Text;

namespace MovieRestApiWithEF.API.Extensions
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
            services.ConfigureOptions<JwtOptionsSetup>();
            services.AddScoped<IJwtService, JwtService>();
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            // Inject an IoC repository container into app that manages access to all repositories
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void AddJwtAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            var jwtTokenConfig = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret));

            // This registers an authentication service along with the authentication options.
            //
            // The chained method decides how the authentication service will work internally. For this we add
            // a JWT Bearer Handler in our app as the handler for the authentication. This validates tokens from
            // all incoming requests.
            //
            // This handler internally invokes the AuthenticateAsync method of JWT Bearer with the provided validation params.
            // Once the token is validated, the User(ClaimsPrinciple) is automatically added to the context and can be
            // accessed in the controller using User.Identity or HttpContext.User.Claims
            //
            // JwtBearerDefaults.AuthenticationScheme is the default scheme used by the handler to authenticate the user.
            // It helps to decide how to construct user identity in case of successful authentication and to
            // decide how to check auth header exists and return 401 if it is missing
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Sets this for AuthenticationScheme and ChallengeScheme
                .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.RequireHttpsMetadata = true;
                jwtBearerOptions.SaveToken = true;
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
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

        public static void AddSwaggerGenWithAuth(this IServiceCollection services)
        {
            var version = "v1";
            services.AddSwaggerGen(options =>
            {
                // This is to generate the Default UI of Swagger Documentation  
                options.SwaggerDoc(version, new OpenApiInfo
                {
                    Version = version,
                    Title = "Movie App REST API",
                    Description = "ASP.NET 6 Web API"
                });

                var authScheme = JwtBearerDefaults.AuthenticationScheme;
                var bearerFormat = "JWT";
                var headerName = "Authorization";

                // To Enable authorization using Swagger (JWT)  
                options.AddSecurityDefinition(authScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Name = headerName,
                    Scheme = authScheme,
                    BearerFormat = bearerFormat,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter your token in the text input below. Don't add 'Bearer' \r\n\r\nExample: \"12345abcdef\"",
                });

                var reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = authScheme };
                var openApiScheme = new OpenApiSecurityScheme { Reference = reference };

                // Adds the auth header globally on all requests
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { openApiScheme, Array.Empty<string>() }
                });
            });
        }

        public static void AddRoleBasedAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // To check authentication on all routes without need for [Authorize] attributes 
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                // To allow admin only on specified routes
                options.AddPolicy("AdminOnly",
                     policy => policy.RequireRole(UserRole.Admin.Name()));
            });
        }

        public static void AddControllersWithFilters(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.Filters.Add(new ActionResponseFilter());
            });
        }

        public static void AddFilterAttributes(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilter>(); // Inject validation filter for use as attribute
            services.Configure<ApiBehaviorOptions>(options =>
            {
                // To disable validation performed by ApiController attribute
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}
