using CDN.Common.Interfaces;
using CDN.Common.Models;
using CDN.Persistance.Entities;
using CDN.Persistance.Extensions;
using CDN.Persistance.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDN.Persistance.Repositories
{
    public sealed class ImageRepository : IImageRepository
    {
        private readonly ICDNContext context;

        public ImageRepository(ICDNContext spaceContext)
        {
            this.context = spaceContext;
        }


        #region IImageRepository Members

        public async Task<IEnumerable<Image>> GetAllAsync()
        {
            return (await this.context.GetCollection().Find(_ => true).ToListAsync()).ToModel();
        }

        public async Task<Image> GetAsync(Guid blobId)
        {
            return (await this.context.GetCollection().Find(imageEntity => imageEntity.BlobId == blobId).FirstOrDefaultAsync()).ToModel();
        }

        public async Task<Image> AddAsync(ImageCreationInfo imageCreationInfo)
        {
            var imageEntity = new ImageEntity
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                BlobId = imageCreationInfo.BlobId,
                Blob = imageCreationInfo.Blob
            };

            await context.GetCollection().InsertOneAsync(imageEntity);

            return imageEntity.ToModel();
        }

        #endregion
    }
}