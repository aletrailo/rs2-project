using CDN.Api.Dtos;
using CDN.Common.Models;

namespace CDN.Api.Extensions
{
    internal static class ImageExtensions
    {
        internal static ImageDto ToDto(this Image model)
        {
            return new ImageDto
            {
                Id = model.Id,
                BlobId = model.BlobId,
                Blob = model.Blob
            };
        }

        internal static IEnumerable<ImageDto> ToDto(this IEnumerable<Image> models)
        {
            return models.Select(model => model.ToDto());
        }

        internal static Image ToModel(this ImageDto dto)
        {
            return new Image
            {
                Id = dto.Id,
                BlobId = dto.BlobId,
                Blob = dto.Blob
            };
        }
    }
}
