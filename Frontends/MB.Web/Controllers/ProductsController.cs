﻿using MB.Shared.Services;
using MB.Web.Models.Catalog;
using MB.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;
using System.ComponentModel;

namespace MB.Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public ProductsController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllProductsAsync());
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoriesAsync();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateInput productCreateInput)
        {
            var categories = await _catalogService.GetAllCategoriesAsync();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            productCreateInput.Picture ??= string.Empty;
            productCreateInput.Feature.Author ??= string.Empty;
            productCreateInput.Feature.ISBN ??= string.Empty;
            productCreateInput.Feature.PublishedDate ??= DateTime.MinValue;

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _catalogService.CreateProductAsync(productCreateInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var product = await _catalogService.GetProductByIdAsync(id);
            var categories = await _catalogService.GetAllCategoriesAsync();

            if (product==null) 
            {
                //TO DO alert gösterilecek
                return RedirectToAction(nameof(Index));
            }

            ViewBag.categoryList = new SelectList(categories, "Id", "Name", product.Id);

            ProductUpdateInput productUpdateInput = new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Picture = product.Picture,
                CategoryId = product.CategoryId,
                Feature = product.Feature,
                //Feature = new FeatureViewModel
                //{
                //    ISBN = product.Feature.ISBN,
                //    Author = product.Feature.Author,
                //    PublishedDate = product.Feature.PublishedDate
                //}
            };

            productUpdateInput.Picture ??= string.Empty;
            productUpdateInput.Feature.Author ??= string.Empty;
            productUpdateInput.Feature.ISBN ??= string.Empty;
            productUpdateInput.Feature.PublishedDate ??= DateTime.MinValue;

            return View(productUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateInput productUpdateInput)
        {
            var categories = await _catalogService.GetAllCategoriesAsync();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name", productUpdateInput.Id);

            productUpdateInput.Picture ??= string.Empty;
            productUpdateInput.Feature.Author ??= string.Empty;
            productUpdateInput.Feature.ISBN ??= string.Empty;
            productUpdateInput.Feature.PublishedDate ??= DateTime.MinValue;

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _catalogService.UpdateProductAsync(productUpdateInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteProductAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
