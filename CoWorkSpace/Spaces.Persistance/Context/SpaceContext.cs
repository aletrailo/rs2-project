using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Spaces.Persistance.Entities;
using Spaces.Persistance.Helpers;
using Spaces.Persistance.Interfaces;

namespace Spaces.Persistance.Context
{
    public class SpaceContext : ISpaceContext
    {
        private readonly MongoClient client;
        private readonly SpacesDbSettings settings;
        
        public SpaceContext(IOptions<SpacesDbSettings> settings)
        {
            this.settings = settings.Value;
            this.settings.ConnectionString = "mongodb://spacesdb:27017"; //kada se pokrece van dokera zakomentarisi ovu liniju
            this.client = new MongoClient(this.settings.ConnectionString);
        }

        #region ISpaceContext Members

        public IMongoCollection<SpaceEntity> GetCollection()
        {
            return this.client.GetDatabase(this.settings.DatabaseName)
                              .GetCollection<SpaceEntity>(this.settings.SpacesCollectionName);
        }

        #endregion
    }
}
