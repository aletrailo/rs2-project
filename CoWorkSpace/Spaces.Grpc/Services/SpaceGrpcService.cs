using Grpc.Core;
using Spaces.Common.Interfaces;
using Spaces.Grpc.Extensions;
using Spaces.Grpc.Protos;

using SpaceModel = Spaces.Common.Models.Space;

namespace Spaces.Grpc.Services
{
    public sealed class SpaceGrpcService : SpaceProtoService.SpaceProtoServiceBase
    {
        private readonly ISpaceService spaceService;

        public SpaceGrpcService(ISpaceService spaceService)
        {
            this.spaceService = spaceService;
        }

        #region SpaceProtoServiceBase Overrides

        public override async Task<GetSpacesResponse> GetSpaces(GetSpacesRequest request, ServerCallContext context)
        {
            IEnumerable<SpaceModel> spaceModels = await this.spaceService.GetAllAsync();

            var response = new GetSpacesResponse();
            response.Spaces.AddRange(spaceModels.ToGrpsModel());

            return response;
        }

        #endregion
    }
}
