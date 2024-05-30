using MB.Web.Models.Catalog;
using MB.Web.Models;
using MB.Web.Models.Order;
using MB.Web.Services;
using MB.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MB.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;

        public OrderController(IOrderService orderService, IBasketService basketService)
        {
            _orderService = orderService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();

            var basketItemCount = 0;

            foreach (var item in basket.BasketItems)
            {

                basketItemCount += item.Quantity;
            }

            ViewBag.BasketItemCount = basketItemCount;
            ViewBag.Basket = basket;

            return View(new CheckoutInfoInput());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
        {
            //var orderStatus = await _orderService.CreateOrder(checkoutInfoInput); // Senkron iletişimde kullanılan kod

            var orderStatus = await _orderService.SuspendOrder(checkoutInfoInput); // Asenkron iletişimde kullanılan kod

            if (!orderStatus.IsSuccessful)
            {
                var basket = await _basketService.Get();

                ViewBag.Basket = basket;

                ViewBag.Error = orderStatus.Error;

                return View();
            }

            return RedirectToAction(nameof(ReceivingPayment));
        }

        public IActionResult ReceivingPayment()
        {
            return View();
        }

        public async Task<IActionResult> SuccessfulCheckoutAsync()
        {
            var orders = await _orderService.GetAllOrders();

            var orderId2 = orders.OrderByDescending(x => x.CreatedDate).FirstOrDefault().Id;

            ViewBag.OrderId = orderId2;

            return View();
        }

        public async Task<IActionResult> CheckoutHistory(int? pageNumber)
        {
            int pageSize = 5;

            var orders = await _orderService.GetOrder();

            orders = orders.OrderByDescending(x => x.CreatedDate).ToList();

            var queryableOrders = orders.AsQueryable();

            return View(PaginatedList<OrderViewModel>.Create(queryableOrders, pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> GetAllOrders(string sortOrder, int? pageNumber, string searchQuery)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchQuery;
            ViewData["IdSortParm"] = sortOrder == "Id" ? "id_desc" : "Id";
            ViewData["CreatedDateSortParm"] = sortOrder == "CreatedDate" ? "CreatedDate_desc" : "CreatedDate";
            ViewData["BuyerIdSortParm"] = sortOrder == "BuyerId" ? "BuyerId_desc" : "BuyerId";

            var orders = await _orderService.GetAllOrders();
            var queryableorders = orders.AsQueryable();

            if (!String.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                queryableorders = queryableorders.Where(u =>
                    u.Id.ToString().ToLower().Contains(searchQuery) ||
                    u.CreatedDate.ToString().ToLower().Contains(searchQuery) ||
                    u.BuyerId.ToString().ToLower().Contains(searchQuery));
            }

            switch (sortOrder)
            {
                case "Id":
                    queryableorders = queryableorders.OrderBy(p => p.Id);
                    break;
                case "id_desc":
                    queryableorders = queryableorders.OrderByDescending(p => p.Id);
                    break;
                case "CreatedDate":
                    queryableorders = queryableorders.OrderBy(p => p.CreatedDate);
                    break;
                case "CreatedDate_desc":
                    queryableorders = queryableorders.OrderByDescending(p => p.CreatedDate);
                    break;
                case "BuyerId":
                    queryableorders = queryableorders.OrderBy(p => p.BuyerId);
                    break;
                case "BuyerId_desc":
                    queryableorders = queryableorders.OrderByDescending(p => p.BuyerId);
                    break;
                default:
                    queryableorders = queryableorders.OrderByDescending(p => p.Id);
                    break;
            }

            int pageSize = 5;
            return View(PaginatedList<OrderViewModel>.Create(queryableorders, pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> GetOrderById(int id)
        {
            var orders = await _orderService.GetAllOrders();

            return View(orders.FirstOrDefault(x => x.Id == id));
        }
    }
}
