using Grpc.Core;
using Spaces.Common.Interfaces;
using Spaces.Common.Models;
using Spaces.Grpc.Extensions;
using Spaces.Grpc.Protos;

using SpaceModel = Spaces.Common.Models.Space;
using SpaceGrpcModel = Spaces.Grpc.Protos.Space;
using CreationInfoModel = Spaces.Common.Models.CreationInfo;

namespace Spaces.Grpc.Services
{
    public sealed class SpaceGrpcService : SpaceProtoService.SpaceProtoServiceBase
    {
        private readonly ISpaceService spaceService;
        private readonly ICDNImageService cdnImageService;

        public SpaceGrpcService(ISpaceService spaceService, ICDNImageService cdnImageService)
        {
            this.spaceService = spaceService;
            this.cdnImageService = cdnImageService;
        }

        #region SpaceProtoServiceBase Overrides

        public override async Task<GetSpacesResponse> GetSpaces(GetSpacesRequest request, ServerCallContext context)
        {
            var response = new GetSpacesResponse();
            
            foreach (var spaceModel in await this.spaceService.GetAllAsync())
            {
                string imageBlob = (await this.cdnImageService.GetAsync(spaceModel.ImageId)).Blob;

                response.Spaces.Add(new SpaceGrpcModel
                {
                    Id = spaceModel.Id,
                    Name = spaceModel.Name,
                    Address = spaceModel.Address,
                    Description = spaceModel.Description,
                    ImageId = spaceModel.ImageId.ToString(),
                    Image = imageBlob,
                    Isfree = spaceModel.IsFree,
                    Priceperhour = spaceModel.PricePerHour,
                    Owner = spaceModel.Owner
                });
            }

            return response;
        }

        public override async Task<InsertSpaceResponse> InsertSpace(InsertSpaceRequest request,ServerCallContext context)
        {

            CreationInfoModel creationInfoModel = request.Space.ToModel();

            try
            {
                Guid imageId = Guid.NewGuid();
                var cdnImageCreationInfo = new CDNImageCreationInfo
                {
                    BlobId = imageId,
                    Blob = creationInfoModel.Image
                };
                await this.cdnImageService.AddAsync(cdnImageCreationInfo);
                await this.spaceService.AddAsync(creationInfoModel, imageId);

                return new InsertSpaceResponse { Response = true };
            }
            catch
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
