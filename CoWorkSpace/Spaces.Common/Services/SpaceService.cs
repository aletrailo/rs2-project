using Spaces.Common.Interfaces;
using Spaces.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spaces.Common.Services
{
    public class SpaceService : ISpaceService
    {
        private readonly ISpaceRepository repository;

        public SpaceService(ISpaceRepository spaceRepository)
        {
            this.repository = spaceRepository;
        }

        #region ISpaceService Members

        public async Task<IEnumerable<Space>> GetAllAsync()
        {
            return await this.repository.GetAllSpaces();
        }

        public Task AddAsync(CreationInfo creationInfo, Guid imageId)
        {
            return this.repository.AddSpaceAsync(creationInfo, imageId);
        }

        public async Task<Space> GetByIdAsync(string Id)
        {
            Space space = await this.repository.GetSpaceByIdAsync(Id);
            return space;
        }

        public Task<bool> UpdateAsync(Space space)
        {
            return this.repository.UpdateSpaceAsync(space);
        }


        public Task<bool> DeleteAsync(string Id)
        {
            return this.repository.DeleteSpaceAsync(Id);
        }

        public void DeleteAllFromDatabase()
        {
            this.repository.DeleteAllromDatabase();
        }

        #endregion
    }
}
