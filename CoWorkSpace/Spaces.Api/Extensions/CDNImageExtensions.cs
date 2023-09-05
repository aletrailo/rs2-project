using Spaces.Common.Models;

using ImageClientModel = CDN.Grpc.Protos.Image;

namespace Spaces.Api.Extensions
{
    internal static class CDNImageExtensions
    {
        internal static CDNImage ToModel(this ImageClientModel clientModel)
        {
            return new CDNImage
            {
                Id = clientModel.Id,
                BlobId = Guid.Parse(clientModel.BlobId),
                Blob = clientModel.Blob
            };
        }

        internal static ImageClientModel ToGrpcModel(this CDNImage model)
        {
            return new ImageClientModel
            {
                Id = model.Id,
                BlobId = model.BlobId.ToString(),
                Blob = model.Blob
            };
        }
    }
}
