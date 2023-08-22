using Advertising.Common.Interfaces;
using Advertising.Common.Models;
using Grpc.Core;
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
                adSpaces.Add(new AdSpace { Name = space.Name, Address = space.Address , Description=space.Description, Image=space.Image});

            return adSpaces;
        }

        public async Task<bool> AddAsync(AdSpace adSpace)
        {

            try
            {
                var space = new SpaceInfo { Name = adSpace.Name, Address = adSpace.Address, Description = adSpace.Description, Image = adSpace.Image };
                var request = new InsertSpaceRequest
                {
                    Space = space
                };
                InsertSpaceResponse insertSpaceResponse = await this.spaceProtoServiceClient.InsertSpaceAsync(request);

                return insertSpaceResponse.Response;
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"Greška prilikom ubacivanja prostora: {ex.Status}");
                return false; 
            }

        }

        #endregion
    }
}
