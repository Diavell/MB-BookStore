using MB.Shared.Dtos;
using MB.Web.Helpers;
using MB.Web.Models;
using MB.Web.Models.Catalog;
using MB.Web.Services.Interfaces;

namespace MB.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateProductAsync(ProductCreateInput productCreateInput)
        {
            var resultPhoto = await _photoStockService.UploadPhoto(productCreateInput.PhotoFormFile);

            if (resultPhoto != null)
            {
                productCreateInput.Picture = resultPhoto.Url;
            }

            var response = await _httpClient.PostAsJsonAsync<ProductCreateInput>("products", productCreateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProductAsync(string productId)
        {
            var response = await _httpClient.DeleteAsync($"products/{productId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("categories");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var categories = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return categories.Data;
        }

        public async Task<List<ProductViewModel>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync("products");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var products = await response.Content.ReadFromJsonAsync<Response<List<ProductViewModel>>>();

            products.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelper.GetPhotoUrl(x.Picture);
            });

            return products.Data;
        }

        public async Task<ProductViewModel> GetProductByIdAsync(string productid)
        {
            var response = await _httpClient.GetAsync($"products/{productid}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var products = await response.Content.ReadFromJsonAsync<Response<ProductViewModel>>();

            products.Data.StockPictureUrl = _photoHelper.GetPhotoUrl(products.Data.Picture);

            return products.Data;
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateInput productUpdateInput)
        {
            var resultPhoto = await _photoStockService.UploadPhoto(productUpdateInput.PhotoFormFile);

            if (resultPhoto != null)
            {
                await _photoStockService.DeletePhoto(productUpdateInput.Picture);
                productUpdateInput.Picture = resultPhoto.Url;
            }

            var response = await _httpClient.PutAsJsonAsync<ProductUpdateInput>("products", productUpdateInput);

            return response.IsSuccessStatusCode;
        }
    }
}
