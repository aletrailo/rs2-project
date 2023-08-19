using MongoDB.Driver;
using Spaces.Persistance.Entities;

namespace Spaces.Persistance.Interfaces
{
    public interface ISpaceContext
    {
        IMongoCollection<SpaceEntity> GetCollection();
    }
}
