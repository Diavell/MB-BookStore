using System.ComponentModel.DataAnnotations;

namespace MB.Web.Models.Catalog
{
    public class ProductUpdateInput
    {
        public string Id { get; set; }

        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Display(Name = "Product Category")]
        public string CategoryId { get; set; }

        public string? Picture { get; set; }

        [Display(Name = "Product Price")]
        public decimal? Price { get; set; }

        [Display(Name = "Product Description")]
        public string Description { get; set; }

        public FeatureViewModel Feature { get; set; }

        [Display(Name = "Product Picture")]
        public IFormFile PhotoFormFile { get; set; }
    }
}
