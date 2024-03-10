namespace MB.Services.Catalog.Dtos
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
        public FeatureDto Feature { get; set; }
    }
}
