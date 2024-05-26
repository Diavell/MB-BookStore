using MB.Shared.Services;
using MB.Web.Models;
using MB.Web.Models.Catalog;
using MB.Web.Services;
using MB.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MB.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CategoriesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 5;
            var categories = await _catalogService.GetAllCategoriesAsync();
            var queryableCategories = categories.AsQueryable();
            return View(PaginatedList<CategoryViewModel>.Create(queryableCategories, pageNumber ?? 1, pageSize));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateInput categoryCreateInput)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _catalogService.CreateCategoryAsync(categoryCreateInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var product = await _catalogService.GetCategoryByIdAsync(id);

            if (product == null)
            {
                //TO DO alert gösterilecek
                return RedirectToAction(nameof(Index));
            }

            CategoryUpdateInput categoryUpdateInput = new()
            {
                Id = product.Id,
                Name = product.Name
            };

            return View(categoryUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateInput categoryUpdateInput)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _catalogService.UpdateCategoryAsync(categoryUpdateInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCategoryAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
