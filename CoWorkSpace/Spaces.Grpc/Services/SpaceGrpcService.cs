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
                if (space == null)
                {
                    return new BookASpaceResponse { Response = false };
                }
                space.IsFree = false;
                space.ReservedBy = username;
                await this.spaceService.UpdateAsync(space);
                return new BookASpaceResponse { Response = true };
            }
            catch(Exception ex)
            {
                return new BookASpaceResponse { Response = false };
            }
        }

        public override async Task<EndUpUsingSpaceResponse> EndUpUsingSpace(EndUpUsingSpaceRequest request, ServerCallContext context)
        {
            string spaceId = request.Spaceid;

            try
            {
                SpaceModel space = await this.spaceService.GetByIdAsync(spaceId);

                if (space == null)
                {
                    return new EndUpUsingSpaceResponse { Response = false };
                }
                space.IsFree = true;
                space.ReservedBy = null;
                await this.spaceService.UpdateAsync(space);
                return new EndUpUsingSpaceResponse { Response = true };

            }
            catch (Exception ex)
            {
                return new EndUpUsingSpaceResponse { Response = false };
            }
        }


        public override async Task<DeleteAdResponse> DeleteAd(DeleteAdRequest request, ServerCallContext context)
        {
            string username = request.Deleteinfo.Username;
            string spaceId = request.Deleteinfo.Spaceid;

            try
            {
                SpaceModel space = await this.spaceService.GetByIdAsync(spaceId);
                if (!space.Owner.Equals(username) || space==null )
                {
                    return new DeleteAdResponse { Response = false };
                }
                await this.spaceService.DeleteAsync(spaceId);
                return new DeleteAdResponse { Response = true };
                
            }
            catch(Exception ex)
            {
                return new DeleteAdResponse { Response = false };
            }

        }

        #endregion
    }
}
