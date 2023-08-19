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
            return this.repository.DeleteSpace(Id);
        }

        #region ISpaceService Members

        public Task<IEnumerable<Space>> GetAllAsync()
        {
            return this.repository.GetAllSpaces();
        }

        public Task<Space> GetByIdAsync(string Id)
        {
            return this.repository.GetSpaceById(Id);
        }

        public Task InsertAsync(Space space)
        {
            return this.repository.InsertSpace(space);
        }

        public Task<bool> UpdateAsync(Space space)
        {
            return this.repository.UpdateSpace(space);
        }

        #endregion
    }
}
