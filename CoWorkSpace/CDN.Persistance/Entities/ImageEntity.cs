using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace CDN.Persistance.Entities
{
    public sealed class ImageEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("BlobId")]
        [BsonRepresentation(BsonType.String)]
        public Guid BlobId { get; set; }

        [BsonElement("Blob")]
        public string Blob { get; set; }
    }
}
