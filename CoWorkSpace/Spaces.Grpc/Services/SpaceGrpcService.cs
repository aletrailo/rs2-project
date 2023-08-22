using Grpc.Core;
using Spaces.Common.Interfaces;
using Spaces.Grpc.Extensions;
using Spaces.Grpc.Protos;

using SpaceModel = Spaces.Common.Models.Space;
using CreationInfoModel = Spaces.Common.Models.CreationInfo;
using Spaces.Common.Models;

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

        public override async Task<InsertSpaceResponse> InsertSpace(InsertSpaceRequest request,ServerCallContext context)
        {

            var ci=request.Space.ToCreationInfo();

            try
            {
                await this.spaceService.InsertAsync(ci);
                return new InsertSpaceResponse { Response = true };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška prilikom ubacivanja prostora: {ex.Message}");
                return new InsertSpaceResponse { Response = false };
            }
        }

        #endregion
    }
}
