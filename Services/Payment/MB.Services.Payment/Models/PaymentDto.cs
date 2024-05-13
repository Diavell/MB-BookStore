namespace MB.Services.Payment.Models
{
    public class PaymentDto
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderDto Order { get; set; }
    }
}
