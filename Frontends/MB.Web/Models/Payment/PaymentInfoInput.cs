namespace MB.Web.Models.Payment
{
    public class PaymentInfoInput
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
