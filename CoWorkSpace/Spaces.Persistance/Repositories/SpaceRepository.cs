using MongoDB.Driver;
using Spaces.Common.Interfaces;
using Spaces.Common.Models;
using Spaces.Persistance.Entities;
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
        
        public async Task<bool> DeleteSpace(string Id)
        {
            var deleteResult = await context.GetCollection().DeleteOneAsync(p => p.Id == Id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }

        public async Task<Space> GetSpaceById(string Id)
        {
            return (await context.GetCollection().Find(p => p.Id == Id).FirstOrDefaultAsync()).ToModel();

        }

        public async Task InsertSpace(Space space)
        {
            await context.GetCollection().InsertOneAsync(space.FromModel());
        }

        public async Task<bool> UpdateSpace(Space spaceN)
        {
            var space=spaceN.FromModel();
            var updateResult = await context.GetCollection().ReplaceOneAsync(p => p.Id == space.Id, space);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;

        }

        #endregion
    }
}
