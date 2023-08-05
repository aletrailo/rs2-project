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

        #region ISpaceService Members

        public Task<IEnumerable<Space>> GetAllAsync()
        {
            return this.repository.GetAllSpaces();
        }

        #endregion
    }
}
