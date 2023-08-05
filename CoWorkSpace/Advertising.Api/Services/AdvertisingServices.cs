using Advertising.Common.Interfaces;
using Advertising.Common.Models;
using Spaces.Grpc.Protos;

namespace Advertising.Api.Services
{
    public sealed class AdvertisingServices : IAdvertisingService
    {
        private readonly SpaceProtoService.SpaceProtoServiceClient spaceProtoServiceClient;

        public AdvertisingServices(SpaceProtoService.SpaceProtoServiceClient spaceProtoServiceClient)
        {
            this.spaceProtoServiceClient = spaceProtoServiceClient;
        }

        #region IAdvertisingService Members

        public async Task<IEnumerable<AdSpace>> GetAllAsync()
        {
            var request = new GetSpacesRequest();

            GetSpacesResponse getSpacesResponse = await this.spaceProtoServiceClient.GetSpacesAsync(request);

            var adSpaces = new List<AdSpace>();

            foreach(var space in getSpacesResponse.Spaces)
                adSpaces.Add(new AdSpace { Name = space.Name, Address = space.Address });

            return adSpaces;
        }

        #endregion
    }
}
