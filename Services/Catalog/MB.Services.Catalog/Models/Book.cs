using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MB.Services.Catalog.Models
{
    public class Bookk
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        public string Picture { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
        public Feature Feature { get; set; }

        [BsonIgnore]
        public Category Category { get; set; }
    }
}
