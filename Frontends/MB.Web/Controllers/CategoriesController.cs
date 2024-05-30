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

        public async Task<IActionResult> Index(string sortOrder, int? pageNumber, string searchQuery)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchQuery;
            ViewData["IdSortParm"] = sortOrder == "Id" ? "id_desc" : "Id";

            var categories = await _catalogService.GetAllCategoriesAsync();
            var queryablecategories = categories.AsQueryable();

            if (!String.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                queryablecategories = queryablecategories.Where(c =>
                    c.Id.ToString().ToLower().Contains(searchQuery) ||
                    c.Name.ToString().ToLower().Contains(searchQuery));
            }

            switch (sortOrder)
            {
                case "Id":
                    queryablecategories = queryablecategories.OrderBy(p => p.Id);
                    break;
                case "id_desc":
                    queryablecategories = queryablecategories.OrderByDescending(p => p.Id);
                    break;
                default:
                    queryablecategories = queryablecategories.OrderBy(p => p.Id);
                    break;
            }

            int pageSize = 5;
            return View(PaginatedList<CategoryViewModel>.Create(queryablecategories, pageNumber ?? 1, pageSize));
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
