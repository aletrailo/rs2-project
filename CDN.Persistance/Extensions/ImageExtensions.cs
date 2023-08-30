
using CDN.Common.Models;
using CDN.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CDN.Persistance.Extensions
{
    internal static class ImageExtensions
    {
        public static Image ToModel(this ImageEntity entity)
        {
            return new Image
            {
                Id = entity.Id,
                BlobId = entity.BlobId,
                Blob = entity.Blob
            };
        }

        public static IEnumerable<Image> ToModel(this IEnumerable<ImageEntity> entities)
        {
            return entities.Select(entity => entity.ToModel());
        }

        public static ImageEntity ToEntity(this Image model)
        {
            return new ImageEntity
            {
                Id = model.Id,
                BlobId = model.BlobId,
                Blob = model.Blob
            };
        }

    }
}
