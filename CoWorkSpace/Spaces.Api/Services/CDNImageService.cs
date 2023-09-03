using CDN.Grpc.Protos;
using Spaces.Api.Extensions;
using Spaces.Common.Interfaces;
using Spaces.Common.Models;

namespace Spaces.Api.Services
{
    public sealed class CDNImageService : ICDNImageService
    {
        private readonly ImageProtoService.ImageProtoServiceClient imageProtoServiceClient;

        public CDNImageService(ImageProtoService.ImageProtoServiceClient imageProtoServiceClient)
        {
            this.imageProtoServiceClient = imageProtoServiceClient;
        }

        #region ICDNImageService Members

        public async Task AddAsync(CDNImageCreationInfo cdnImageCreationInfo)
        {
            var request = new InsertImageRequest
            {
                ImageCreationInfo = new ImageCreationInfo
                {
                    BlobId = cdnImageCreationInfo.BlobId.ToString(),
                    Blob = cdnImageCreationInfo.Blob
                }
            };

            await this.imageProtoServiceClient.InsertImageAsync(request);
        }

        public async Task<CDNImage> GetAsync(Guid id)
        {
            var request = new GetImageRequest();
            request.BlobId = id.ToString();

            GetImageResponse response = await this.imageProtoServiceClient.GetImageAsync(request);

            return response.Image.ToModel();
        }

        #endregion
    }
}
