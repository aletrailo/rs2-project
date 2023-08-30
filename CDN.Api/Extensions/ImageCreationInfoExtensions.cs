using CDN.Api.Dtos;
using CDN.Common.Models;

namespace CDN.Api.Extensions
{
    internal static class ImageCreationInfoExtensions
    {
        internal static ImageCreationInfo ToModel(this ImageCreationInfoDto dto)
        {
            return new ImageCreationInfo
            {
                BlobId = dto.BlobId,
                Blob = dto.Blob
            };
        }
    }
}
