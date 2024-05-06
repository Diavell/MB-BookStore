using MB.Web.Models.Basket;
using MB.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MB.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public BasketController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _basketService.Get());
        }

        public async Task<IActionResult> AddBasketItem(string productId)
        {
            var product = await _catalogService.GetProductByIdAsync(productId);

            if (product != null)
            {
                var basketItem = new BasketItemViewModel
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1,
                    Picture = product.StockPictureUrl
                    //DiscountAppliedPrice = 0
                };

                await _basketService.AddBasketItem(basketItem);

                ViewBag.Picture = product.StockPictureUrl;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveBasketItem(string productId)
        {
            await _basketService.RemoveBasketItem(productId);

            return RedirectToAction(nameof(Index));
        } 

        public async Task<IActionResult> IncreaseQuantity(string productId)
        {
            await _basketService.IncreaseQuantity(productId);

            return RedirectToAction(nameof(Index));
        } 

        public async Task<IActionResult> DecreaseQuantity(string productId)
        {
            await _basketService.DecreaseQuantity(productId);

            return RedirectToAction(nameof(Index));
        }
    }
}
