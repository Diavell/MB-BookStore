using MB.Web.Exceptions;
using MB.Web.Models;
using MB.Web.Services.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MB.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICatalogService _catalogService;

        public HomeController(ILogger<HomeController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllProductsAsync());
        }

        [HttpGet]
        public async Task<IActionResult> SearchProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View();
            }

            var response = await _catalogService.GetAllProductsAsync();

            if (response == null)
            {
                return View();
            }

            var productList = response.Where(x => x.Name.ToLower().Contains(id.ToLower())).ToList();

            if (productList.Count == 0)
            {
                return View();
            }

            return View(productList);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View();
            }

            return View(await _catalogService.GetProductByIdAsync(id));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (errorFeature != null && errorFeature.Error is UnAuthorizeException)
            {
                _logger.LogError(errorFeature.Error, "An error occurred");

                return RedirectToAction(nameof(AuthController.LogOut), "Auth");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
