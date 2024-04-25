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

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Display(Name = "Line")]
        public string Line { get; set; }

        [Display(Name = "Card Holder Name")]
        public string CardHolderName { get; set; }

        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }
        
        [Display(Name = "Expiry Date Month")]
        public DateTime ExpiryDateMonth { get; set; }
        
        [Display(Name = "Expiry Date Year")]
        public DateTime ExpiryDateYear { get; set; }

        [Display(Name = "CVV")]
        public string CVV { get; set; }
    }
}
