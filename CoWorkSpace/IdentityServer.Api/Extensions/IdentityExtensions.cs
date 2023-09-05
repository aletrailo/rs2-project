using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using IdentityServer.Api.Data;
using Microsoft.EntityFrameworkCore;
using IdentityServer.Api.Enitities;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using IdentityServer.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IdentityServer.Api.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection ConfigurePersistence(this IServiceCollection servies, IConfiguration configuration) {
            servies.AddDbContext<ApplicationContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString"));
            });

            return servies;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services) {
            services.AddIdentity<User, IdentityRole>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            return services;
         }

        public static IServiceCollection ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetSection("secretKey").Value;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                        ValidAudience = jwtSettings.GetSection("validAudience").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });

            return services;
        }

        public static IServiceCollection ConfigureMiscellaneousService(this IServiceCollection services) {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddCors(options =>
            {   
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            return services;
        }




    }













}
