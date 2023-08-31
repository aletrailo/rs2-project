using ImageModel = CDN.Common.Models.Image;
using ImageCreationInfoModel = CDN.Common.Models.ImageCreationInfo;
using ImageGrpc = CDN.Grpc.Protos.Image;
using ImageCreationInfoGrpc = CDN.Grpc.Protos.ImageCreationInfo;

namespace Spaces.Grpc.Extensions
{
    internal static class ImageResponsesExtensions
    {
        internal static ImageGrpc ToGrpcModel(this ImageModel model)
        {
            return new ImageGrpc
            {
                Id = model.Id,
                BlobId = model.BlobId.ToString(),
                Blob = model.Blob
            };
        }

        internal static IEnumerable<ImageGrpc> ToGrpcModel(this IEnumerable<ImageModel> models)
        {
            return models.Select(model => model.ToGrpcModel());
        }

        internal static ImageCreationInfoModel ToModel(this ImageCreationInfoGrpc imageCreationInfoGrpc)
        {
            return new ImageCreationInfoModel
            {
                BlobId = Guid.Parse(imageCreationInfoGrpc.BlobId),
                Blob = imageCreationInfoGrpc.Blob
            };
        }
    }
}
