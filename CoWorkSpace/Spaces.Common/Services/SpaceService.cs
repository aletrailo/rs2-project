﻿using Spaces.Common.Interfaces;
using Spaces.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spaces.Common.Services
{
    public class SpaceService : ISpaceService
    {
        private readonly ISpaceRepository repository;
        private readonly ICDNImageService cdnImageService;

        public SpaceService(ISpaceRepository spaceRepository, ICDNImageService cdnImageService)
        {
            this.repository = spaceRepository;
            this.cdnImageService = cdnImageService;
        }

        public Task<bool> DeleteAsync(string Id)
        {
            return this.repository.DeleteSpaceAsync(Id);
        }

        #region ISpaceService Members

        public Task<IEnumerable<Space>> GetAllAsync()
        {
            return this.repository.GetAllSpaces();
        }

        public Task<Space> GetByIdAsync(string Id)
        {
            return this.repository.GetSpaceByIdAsync(Id);
        }

        public Task AddAsync(CreationInfo creationInfo)
        {
            var cdnImageCreationInfo = new CDNImageCreationInfo
            {
                BlobId = Guid.NewGuid(),
                Blob = creationInfo.Image
            };

            this.cdnImageService.AddAsync(cdnImageCreationInfo);
            return this.repository.AddSpaceAsync(creationInfo);
        }

        public Task<bool> UpdateAsync(Space space)
        {
            return this.repository.UpdateSpaceAsync(space);
        }

        #endregion
    }
}
