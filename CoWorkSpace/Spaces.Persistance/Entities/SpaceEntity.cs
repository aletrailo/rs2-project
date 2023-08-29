using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Spaces.Persistance.Entities
{
    public sealed class SpaceEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Address")]
        public string Address { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Image")]
        public string Image { get; set; }

        [BsonElement("IsFree")]
        public bool IsFree { get; set; } = true;

        [BsonElement("PricePerHour")]
        public int PricePerHour { get; set; }

        [BsonElement("Owner")]
        public string Owner { get; set; } = string.Empty;

        [BsonElement("ReservedBy")]
        public string ReservedBy { get; set; } = string.Empty;
    }
}
