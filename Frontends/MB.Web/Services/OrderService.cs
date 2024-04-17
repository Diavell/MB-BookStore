using MB.Shared.Dtos;
using MB.Shared.Services;
using MB.Web.Models.Order;
using MB.Web.Models.Payment;
using MB.Web.Services.Interfaces;

namespace MB.Web.Services
{
    public class OrderService : IOrderService
    {
        public readonly HttpClient _httpClient;
        public readonly IPaymentService _paymentService;
        public readonly IBasketService _basketService;
        public readonly ISharedIdentityService _sharedIdentityService;

        public OrderService(HttpClient httpClient, IPaymentService paymentService, IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _httpClient = httpClient;
            _paymentService = paymentService;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();

            var paymentInfoInput = new PaymentInfoInput()
            {
                CardHolderName = checkoutInfoInput.CardHolderName,
                CardNumber = checkoutInfoInput.CardNumber,
                ExpiryDate = checkoutInfoInput.ExpiryDate,
                CVV = checkoutInfoInput.CVV,
                TotalPrice = basket.TotalPrice
            };

            var paymentResult = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!paymentResult)
            {
                return new OrderCreatedViewModel()
                {
                    Error = "No payment received!",
                    IsSuccessful = false
                };
            }

            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput()
                {
                    Province = checkoutInfoInput.Province,
                    District = checkoutInfoInput.District,
                    Line = checkoutInfoInput.Line,
                    Street = checkoutInfoInput.Street,
                    ZipCode = checkoutInfoInput.ZipCode
                }
            };

            basket.BasketItems.ForEach(x =>
            {
                orderCreateInput.OrderItems.Add(new OrderItemCreateInput()
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    PictureUrl = "",
                    Quantity = x.Quantity
                });
            });

            var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders", orderCreateInput);

            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel()
                {
                    Error = "Order could not be created!",
                    IsSuccessful = false
                };
            }

            var orderCreatedViewModel = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();

            orderCreatedViewModel.Data.IsSuccessful = true;

            await _basketService.Delete();

            return orderCreatedViewModel.Data;
        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");

            return response.Data;
        }

        public Task SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            throw new NotImplementedException();
        }
    }
}
