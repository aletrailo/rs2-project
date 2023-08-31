using Spaces.Common.Interfaces;
using Spaces.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        #region ISpaceService Members

        public async Task<IEnumerable<Space>> GetAllAsync()
        {
            List<Space> spaces = (await this.repository.GetAllSpaces()).ToList();

            foreach (Space space in spaces)
                space.Image = this.cdnImageService.GetAsync(space.ImageId).Blob;

            return spaces;
        }

        public Task AddAsync(CreationInfo creationInfo)
        {
            var cdnImageCreationInfo = new CDNImageCreationInfo
            {
                BlobId = Guid.NewGuid(),
                Blob = creationInfo.Image
            };

            this.cdnImageService.AddAsync(cdnImageCreationInfo);
            return this.repository.AddSpaceAsync(creationInfo, cdnImageCreationInfo.BlobId);
        }

        public async Task<Space> GetByIdAsync(string Id)
        {
            Space space = await this.repository.GetSpaceByIdAsync(Id);
            space.Image = this.cdnImageService.GetAsync(space.ImageId).Blob;

            return space;
        }

        public Task<bool> UpdateAsync(Space space)
        {
            //Add update on CDN.Grpc
            return this.repository.UpdateSpaceAsync(space);
        }


        public Task<bool> DeleteAsync(string Id)
        {
            //Add delete on CDN.Grpc
            return this.repository.DeleteSpaceAsync(Id);
        }

        #endregion
    }
}
