using CDN.Common.Interfaces;
using CDN.Common.Services;
using CDN.Persistance.Context;
using CDN.Persistance.Helpers;
using CDN.Persistance.Interfaces;
using CDN.Persistance.Repositories;

namespace CDN.Api.Extensions
{
    internal static class IServiceCollectionExtensions
    {
        internal static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CDNDbSettings>(configuration.GetSection("CDNDbSettings"));
            services.AddTransient<ICDNContext, CDNContext>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IImageService, ImageService>();
        }
    }
}