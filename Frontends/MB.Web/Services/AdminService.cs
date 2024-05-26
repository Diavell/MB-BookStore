using MB.Shared.Dtos;
using MB.Web.Models;
using MB.Web.Models.Order;
using MB.Web.Services.Interfaces;
using Newtonsoft.Json;
using static MB.Web.Models.AdminReturnResult;

namespace MB.Web.Services
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _httpClient;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly ICatalogService _catalogService;
        
        public AdminService(HttpClient httpClient, IOrderService orderService, IUserService userService, ICatalogService catalogService)
        {
            _httpClient = httpClient;
            _orderService = orderService;
            _userService = userService;
            _catalogService = catalogService;
        }

        public async Task<decimal> Revenues(TimePeriodEnum period)
        {
            var orders = await _orderService.GetAllOrders();

            List<OrderViewModel> filteredOrders;

            DateTime now = DateTime.Now;

            if (period == TimePeriodEnum.Monthly)
            {
                filteredOrders = orders.Where(x => x.CreatedDate.Month == now.Month && x.CreatedDate.Year == now.Year).ToList();
            }
            else
            {
                filteredOrders = orders.Where(x => x.CreatedDate.Year == now.Year).ToList();
            }

            filteredOrders.ForEach(x =>
            {
                x.TotalPrice = x.OrderItems.Sum(item => item.TotalPrice);
            });

            var totalEarnings = filteredOrders.Sum(x => x.TotalPrice);

            return totalEarnings;
        }
        
        public async Task<int> Orders(TimePeriodEnum period)
        {
            var orders = await _orderService.GetAllOrders();

            DateTime now = DateTime.Now;

            List<OrderViewModel> filteredOrders;

            if (period == TimePeriodEnum.Monthly)
            {
                filteredOrders = orders.Where(x => x.CreatedDate.Month == now.Month && x.CreatedDate.Year == now.Year).ToList();
            }
            else
            {
                filteredOrders = orders.Where(x => x.CreatedDate.Year == now.Year).ToList();
            }

            return filteredOrders.Count;
        }

        public async Task<int> NumberOfMembers()
        {
            var response = await _userService.GetAllUsers();

            return response.Count;
        }
        
        public async Task<int> NumberOfProducts()
        {
            var response = await _catalogService.GetAllProductsAsync();

            return response.Count;
        }
        
        public async Task<int> NumberOfCategories()
        {
            var response = await _catalogService.GetAllCategoriesAsync();

            return response.Count;
        }
        
        public async Task<string> MostPopularProduct(TimePeriodEnum period)
        {
            var orders = await _orderService.GetAllOrders();

            DateTime now = DateTime.Now;

            IEnumerable<OrderViewModel> filteredOrders;

            if (period == TimePeriodEnum.Monthly)
            {
                filteredOrders = orders.Where(x => x.CreatedDate.Month == now.Month && x.CreatedDate.Year == now.Year);
            }
            else
            {
                filteredOrders = orders.Where(x => x.CreatedDate.Year == now.Year);
            }

            var orderItems = filteredOrders.SelectMany(order => order.OrderItems);

            var mostPopularProduct = orderItems
                .GroupBy(item => item.ProductId)
                .OrderByDescending(group => group.Sum(item => item.Quantity))
                .FirstOrDefault()?.FirstOrDefault()?.ProductName;

            return mostPopularProduct;
        }

        public async Task<List<DataPoint>> SalesOfProductsChart()
        {
            List<DataPoint> dataPoints = new();

            var orders = await _orderService.GetAllOrders();

            var allOrderItems = orders.SelectMany(order => order.OrderItems);

            var groupedOrderItems = allOrderItems.GroupBy(item => item.ProductId);

            foreach (var group in groupedOrderItems)
            {
                var productName = group.FirstOrDefault()?.ProductName;

                if (productName != null)
                {
                    var totalQuantity = group.Sum(item => item.Quantity);

                    dataPoints.Add(new DataPoint(productName, totalQuantity));
                }
            }

            return dataPoints;
        }

        public async Task<List<DataPoint>> NumberOfOrdersChart()
        {
            List<DataPoint> dataPoints = new();

            var orders = await _orderService.GetAllOrders();

            var groupedOrders = orders.GroupBy(x => x.CreatedDate.Month);

            foreach (var item in groupedOrders)
            {
                var month = item.FirstOrDefault().CreatedDate.ToString("MMMM");

                dataPoints.Add(new DataPoint(month, item.Sum(x => x.OrderItems.Sum(x => x.Quantity))));
            }

            return dataPoints;
        }
        
        public async Task<List<DataPoint>> TotalOrderAmountsChart()
        {
            List<DataPoint> dataPoints = new();

            var orders = await _orderService.GetAllOrders();

            var groupedOrders = orders.GroupBy(x => x.CreatedDate.Month);

            foreach (var item in groupedOrders)
            {
                var month = item.FirstOrDefault().CreatedDate.ToString("MMMM");

                dataPoints.Add(new DataPoint(month, item.Sum(x => ((double)x.OrderItems.Sum(x => x.TotalPrice)))));
            }

            return dataPoints;
        }
    }
}
