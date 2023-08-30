﻿using CDN.Common.Interfaces;
using CDN.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDN.Common.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository repository;

        public ImageService(IImageRepository imageRepository)
        {
                this.repository = imageRepository;
        }

        #region ISpaceService Members

        public Task<IEnumerable<Image>> GetAllAsync()
        {
            return this.repository.GetAllAsync();
        }

        public async Task<Image> GetAsync(Guid blobId)
        {
            return await this.repository.GetAsync(blobId);
        }

        public async Task<Image> AddAsync(ImageCreationInfo imageCreationInfo)
        {
            return await this.repository.AddAsync(imageCreationInfo);
        }

        #endregion
    }
}
