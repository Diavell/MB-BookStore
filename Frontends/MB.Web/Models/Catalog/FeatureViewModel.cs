using System.ComponentModel.DataAnnotations;

namespace MB.Web.Models.Catalog
{
    public class FeatureViewModel
    {
        [Display(Name = "Book Author")]
        public string? Author { get; set; }

        [Display(Name = "Book ISBN")]
        public string? ISBN { get; set; }

        [Display(Name = "Book Pusblished Date")]
        public DateTime? PublishedDate { get; set; }
    }
}
