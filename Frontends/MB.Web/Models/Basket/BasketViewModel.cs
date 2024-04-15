namespace MB.Web.Models.Basket
{
    public class BasketViewModel
    {
        public BasketViewModel()
        {
            _basketItems = new List<BasketItemViewModel>();
        }

        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        //public int? DiscountRate { get; set; }
        private List<BasketItemViewModel> _basketItems { get; set; }
        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                //if (HasDiscount)
                //{
                //    _basketItems.ForEach(x =>
                //    {                         
                //        var discountPrice = x.Price * ((decimal)DiscountRate.Value / 100);
                //        x.AppliedDiscount(Math.Round(x.Price-discountPrice,2));
                //    });
                //}
                return _basketItems;
            }
            set
            {
                _basketItems = value;
            }
        }
        public decimal TotalPrice
        {
            get
            {
                if (_basketItems == null)
                    return 0;

                return _basketItems.Sum(x => x.Price * x.Quantity);
            }
        }

        //public bool HasDiscount
        //{
        //    get => !string.IsNullOrEmpty(DiscountCode);
        //}
    }
}
