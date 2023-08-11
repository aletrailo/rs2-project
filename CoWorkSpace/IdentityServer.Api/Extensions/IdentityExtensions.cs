using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using IdentityServer.Api.Data;
using Microsoft.EntityFrameworkCore;
using IdentityServer.Api.Enitities;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

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

        public static IServiceCollection ConfigureMiscellaneousService(this IServiceCollection services) {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }













}
