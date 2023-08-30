using CDN.Persistance.Entities;
using MongoDB.Driver;

namespace CDN.Persistance.Interfaces
{
    public interface ICDNContext
    {
        IMongoCollection<ImageEntity> GetCollection();
    }
}
