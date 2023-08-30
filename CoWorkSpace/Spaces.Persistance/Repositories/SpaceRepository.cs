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
        
        public async Task<bool> DeleteSpaceAsync(string Id)
        {
            var deleteResult = await context.GetCollection().DeleteOneAsync(p => p.Id == Id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }

        public async Task<Space> GetSpaceByIdAsync(string Id)
        {
            return (await context.GetCollection().Find(p => p.Id == Id).FirstOrDefaultAsync()).ToModel();

        }

        public async Task AddSpaceAsync(CreationInfo creationInfo)
        {
            SpaceEntity spaceEntity =  new SpaceEntity
                {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    Name = creationInfo.Name,
                    Address = creationInfo.Address,
                    Description = creationInfo.Description,
                    Image = creationInfo.Image,
                    IsFree = creationInfo.IsFree,
                    PricePerHour = creationInfo.PricePerHour,
                    Owner = creationInfo.Owner,
 
                };


            await context.GetCollection().InsertOneAsync(spaceEntity);
        }

        public async Task<bool> UpdateSpaceAsync(Space space)
        {
            var spaceEntity = space.ToEntity();
            
            var updateResult = await context.GetCollection().ReplaceOneAsync(p => p.Id == spaceEntity.Id, spaceEntity);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;

        }

        public async Task<IEnumerable<Space>> GetAllReservedByAsync(string username)
        {
            return (await this.context.GetCollection().Find(p => p.ReservedBy == username).ToListAsync()).ToModel();
        }

        public async Task<IEnumerable<Space>> GetAllOwnedByAsync(string username)
        {
            return (await this.context.GetCollection().Find(p => p.Owner == username).ToListAsync()).ToModel();
        }

        #endregion
    }
}
