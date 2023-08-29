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

            var ci=request.Space.ToModel();

            try
            {
                await this.spaceService.AddAsync(ci);
                return new InsertSpaceResponse { Response = true };
            }
            catch (Exception ex)
            {
                
                return new InsertSpaceResponse { Response = false };
            }
        }

        public override async Task<BookASpaceResponse> BookASpace(BookASpaceRequest request, ServerCallContext context)
        {
            string username = request.Reservation.Username;
            string spaceId = request.Reservation.Spaceid;

            try
            {
                SpaceModel space = await this.spaceService.GetByIdAsync(spaceId);
                space.IsFree = false;
                space.ReservedBy = username;
                await this.spaceService.UpdateAsync(space);
                return new BookASpaceResponse { Response = true };
            }catch(Exception ex)
            {
                return new BookASpaceResponse { Response = false };
            }
        }

        #endregion
    }
}
