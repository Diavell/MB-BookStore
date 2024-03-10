using MB.Services.Catalog.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MB.Services.Catalog.Dtos
{
    public class BookDtoo
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CategoryId { get; set; }
        public string ISBN { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public FeatureDto Feature { get; set; }
        public CategoryDto Category { get; set; }
    }
}
