using System.ComponentModel.DataAnnotations;

namespace MB.Web.Models.Catalog
{
    public class ProductUpdateInput
    {
        public string Id { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Product Category")]
        [Required(ErrorMessage = "Category is required")]
        public string CategoryId { get; set; }

        [Display(Name = "Product Picture")]
        //[Required(ErrorMessage = "Picture is required")]
        public string? Picture { get; set; }

        [Display(Name = "Product Price")]
        [Required(ErrorMessage = "Price is required")]
        public decimal? Price { get; set; }

        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public FeatureViewModel Feature { get; set; }
    }
}
