using System.ComponentModel.DataAnnotations;

namespace MB.Web.Models.Order
{
    public class CheckoutInfoInput
    {
        [Display(Name = "Province")]
        public string Province { get; set; }

        [Display(Name = "District")]
        public string District { get; set; }

        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "ZipCode")]
        public string ZipCode { get; set; }

        [Display(Name = "Line")]
        public string Line { get; set; }

        [Display(Name = "CardHolderName")]
        public string CardHolderName { get; set; }

        [Display(Name = "CardNumber")]
        public string CardNumber { get; set; }

        [Display(Name = "ExpiryDate")]
        public string ExpiryDate { get; set; }

        [Display(Name = "CVV")]
        public string CVV { get; set; }
    }
}
