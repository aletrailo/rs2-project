using Spaces.Common.Interfaces;
using Spaces.Common.Services;
using Spaces.Persistance.Context;
using Spaces.Persistance.Helpers;
using Spaces.Persistance.Interfaces;
using Spaces.Persistance.Repositories;

namespace Spaces.Grpc.Extensions
{
    internal static class IServiceCollectionExtensions
    {
        internal static void AddCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SpacesDbSettings>(configuration.GetSection("SpacesDbSettings"));
            services.AddTransient<ISpaceContext, SpaceContext>();
            services.AddTransient<ISpaceRepository, SpaceRepository>();
            services.AddTransient<ISpaceService, SpaceService>();
        }
    }
}
