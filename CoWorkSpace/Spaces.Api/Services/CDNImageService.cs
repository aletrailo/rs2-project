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

        public void AddAsync(CDNImageCreationInfo cdnImageCreationInfo)
        {
            var request = new InsertImageRequest
            {
                ImageCreationInfo = new ImageCreationInfo
                {
                    BlobId = cdnImageCreationInfo.BlobId.ToString(),
                    Blob = cdnImageCreationInfo.Blob
                }
            };

            this.imageProtoServiceClient.InsertImage(request);
        }

        public CDNImage GetAsync(Guid id)
        {
            var request = new GetImageRequest();

            GetImageResponse response = this.imageProtoServiceClient.GetImage(request);

            return response.Image.ToModel();
        }

        #endregion
    }
}
