using CDN.Common.Interfaces;
using CDN.Grpc.Protos;
using Grpc.Core;
using Spaces.Grpc.Extensions;

using ImageModel = CDN.Common.Models.Image;

namespace CDN.Grpc.Services
{
    public sealed class ImageGrpcService : ImageProtoService.ImageProtoServiceBase
    {
        private readonly IImageService imageService;

        public ImageGrpcService(IImageService imageService)
        {
            this.imageService = imageService;
        }

        #region SpaceProtoServiceBase Overrides

        public override async Task<GetImagesResponse> GetImages(GetImagesRequest request, ServerCallContext context)
        {
            IEnumerable<ImageModel> mageModels = await this.imageService.GetAllAsync();

            var response = new GetImagesResponse();
            response.Images.AddRange(mageModels.ToGrpcModel());

            return response;
        }

        public override async Task<GetImageResponse> GetImage(GetImageRequest request, ServerCallContext context)
        {
            ImageModel imageModel = await this.imageService.GetAsync(Guid.Parse(request.BlobId));

            var response = new GetImageResponse();
            response.Image = imageModel.ToGrpcModel();

            return response;
        }

        public override async Task<InsertImageResponse> InsertImage(InsertImageRequest request, ServerCallContext context)
        {
            ImageModel imageModel = await this.imageService.AddAsync(request.ImageCreationInfo.ToModel());

            var response = new InsertImageResponse();
            response.Image = imageModel.ToGrpcModel();

            return response;
        }

        #endregion
    }
}
