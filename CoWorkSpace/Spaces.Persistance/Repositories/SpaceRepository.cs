using MongoDB.Driver;
using Spaces.Common.Interfaces;
using Spaces.Common.Models;
using Spaces.Persistance.Extensions;
using Spaces.Persistance.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spaces.Persistance.Repositories
{
    public sealed class SpaceRepository : ISpaceRepository
    {
        private readonly ISpaceContext context;

        public SpaceRepository(ISpaceContext spaceContext)
        {
            this.context = spaceContext;
        }

        #region ISpaceRepository Members

        public async Task<IEnumerable<Space>> GetAllSpaces()
        {
            return (await this.context.GetCollection().Find(_ => true).ToListAsync()).ToModel();
        }

        #endregion
    }
}
