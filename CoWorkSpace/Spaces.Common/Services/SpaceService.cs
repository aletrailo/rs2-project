using Spaces.Common.Interfaces;
using Spaces.Common.Models;
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
            return this.repository.AddSpaceAsync(creationInfo);
        }

        public Task<bool> UpdateAsync(CreationInfo creationInfo)
        {
            return this.repository.UpdateSpaceAsync(creationInfo);
        }

        #endregion
    }
}
