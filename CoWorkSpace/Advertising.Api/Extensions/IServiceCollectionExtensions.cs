using Advertising.Api.Services;
using Advertising.Common.Interfaces;

namespace Advertising.Api.Extensions
{
    internal static class IServiceCollectionExtensions
    {
        internal static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAdvertisingService, AdvertisingServices>();
        }
    }
}
