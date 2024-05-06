namespace MB.Web.Models.Basket
{
    public class BasketItemViewModel
    {
        public int Quantity { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }

        //public decimal? DiscountAppliedPrice { get; set; }
        //public decimal GetCurrentPrice { get => DiscountAppliedPrice != null ? Price : Price; }
        //public void AppliedDiscount(decimal discount)
        //{
        //    DiscountAppliedPrice = discount;
        //}
    }
}
