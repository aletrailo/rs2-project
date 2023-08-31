using CDN.Persistance.Entities;
using CDN.Persistance.Helpers;
using CDN.Persistance.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CDN.Persistance.Context
{
    public class CDNContext : ICDNContext
    {
        private readonly MongoClient client;
        private readonly CDNDbSettings settings;
        
        public CDNContext(IOptions<CDNDbSettings> settings)
        {
            this.settings = settings.Value;
            this.client = new MongoClient(this.settings.ConnectionString);
        }

        #region ISpaceContext Members

        public IMongoCollection<ImageEntity> GetCollection()
        {
            return this.client.GetDatabase(this.settings.DatabaseName)
                              .GetCollection<ImageEntity>(this.settings.CDNCollectionName);
        }

        #endregion
    }
}
