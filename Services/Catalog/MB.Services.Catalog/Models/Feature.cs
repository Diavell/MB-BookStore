using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MB.Services.Catalog.Models
{
    public class Feature
    {
        public string Author { get; set; }
        public string ISBN { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime PublishedDate { get; set; }
    }
}
