using MB.Shared.Dtos;
using MB.Web.Models;
using MB.Web.Services;
using MB.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static MB.Web.Models.AdminReturnResult;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MB.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IOrderService _orderService;

        public AdminController(IAdminService adminService, IOrderService orderService)
        {
            _adminService = adminService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var monthltyRevenue = await _adminService.Revenues(TimePeriodEnum.Monthly);
            ViewBag.MonthlyEarning = monthltyRevenue;

            var annualRevenue = await _adminService.Revenues(TimePeriodEnum.Annual);
            ViewBag.AnnualEarning = annualRevenue;

            var monthltyOrders = await _adminService.Orders(TimePeriodEnum.Monthly);
            ViewBag.MonthlyOrders = monthltyOrders;

            var annualOrders = await _adminService.Orders(TimePeriodEnum.Annual);
            ViewBag.AnnualOrders = annualOrders;

            var numberOfMembers = await _adminService.NumberOfMembers();
            ViewBag.NumberOfMembers = numberOfMembers;

            var numberOfProducts = await _adminService.NumberOfProducts();
            ViewBag.NumberOfProducts = numberOfProducts;

            var numberOfCategories = await _adminService.NumberOfCategories();
            ViewBag.NumberOfCategories = numberOfCategories;
            
            var mostPopularProductMonthly = await _adminService.MostPopularProduct(TimePeriodEnum.Monthly);
            ViewBag.MostPopularProductMonthly = mostPopularProductMonthly;

            var mostPopularProduct = await _adminService.MostPopularProduct(TimePeriodEnum.Annual);
            ViewBag.MostPopularProductAnnual = mostPopularProduct;

            var salesOfProductsChart = await _adminService.SalesOfProductsChart();
            ViewBag.SalesOfProductsChart = JsonConvert.SerializeObject(salesOfProductsChart);

            var numberOfOrdersChart = await _adminService.NumberOfOrdersChart();
            ViewBag.NumberOfOrdersChart = JsonConvert.SerializeObject(numberOfOrdersChart);
            
            var totalOrderAmountsChart = await _adminService.TotalOrderAmountsChart();
            ViewBag.TotalOrderAmountsChart = JsonConvert.SerializeObject(totalOrderAmountsChart);

            return View();
        }
    }
}
