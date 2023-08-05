using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Spaces.Persistance.Entities
{
    public sealed class SpaceEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public string Address { get; set; }
    }
}
