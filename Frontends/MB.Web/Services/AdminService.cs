using MB.Shared.Dtos;
using MB.Web.Models;
using MB.Web.Models.Order;
using MB.Web.Services.Interfaces;
using Newtonsoft.Json;

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

        public async Task<decimal> MonthltyEarning()
        {            
            var orders = await _orderService.GetAllOrders();

            var monthltyorders = orders.Where(x => x.CreatedDate.Month == DateTime.Now.Month).ToList();

            monthltyorders.ForEach(x =>
            {
                x.TotalPrice = x.OrderItems.Sum(x => x.TotalPrice);
            });

            var monthltyEarning = monthltyorders.Sum(x => x.TotalPrice);

            return monthltyEarning;
        }
        
        public async Task<decimal> AnnualEarning()
        {            
            var orders = await _orderService.GetAllOrders();

            var annualOrders = orders.Where(x => x.CreatedDate.Year == DateTime.Now.Year).ToList();

            annualOrders.ForEach(x =>
            {
                x.TotalPrice = x.OrderItems.Sum(x => x.TotalPrice);
            });

            var annualEarning = annualOrders.Sum(x => x.TotalPrice);

            return annualEarning;
        }
        
        public async Task<int> MonthltyOrders()
        {            
            var orders = await _orderService.GetAllOrders();

            var monthltyOrders = orders.Where(x => x.CreatedDate.Month == DateTime.Now.Month).ToList();

            return monthltyOrders.Count;
        }
        
        public async Task<int> AnnualOrders()
        {            
            var orders = await _orderService.GetAllOrders();

            var annualOrders = orders.Where(x => x.CreatedDate.Year == DateTime.Now.Year).ToList();

            return annualOrders.Count;
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

        public async Task<List<DataPoint>> SalesOfProductsChart()
        {
            List<DataPoint> dataPoints = new();

            var orders = await _orderService.GetAllOrders();

            var groupedOrders = orders.GroupBy(x => x.OrderItems.Select(x => x.ProductId));

            foreach (var item in groupedOrders)
            {
                var product = item.FirstOrDefault().OrderItems.FirstOrDefault().ProductName;

                dataPoints.Add(new DataPoint(product, item.Sum(x => x.OrderItems.Sum(x => x.Quantity))));
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
