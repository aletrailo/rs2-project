using IdentityServer.Api.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace IdentityServer.Api.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetService<ApplicationContext>();

                try
                {
                    var retry = Policy.Handle<SqlException>()
                        .WaitAndRetry(
                            retryCount: 5,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

                    retry.Execute(() => MigrateDatabase(context));
                }
                catch
                {
                    throw;
                }
            }

            return host;
        }

        private static void MigrateDatabase(ApplicationContext context)
        {
           context.Database.Migrate();
        }
    }
}
