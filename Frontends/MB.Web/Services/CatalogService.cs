using MB.Shared.Dtos;
using MB.Web.Models;
using MB.Web.Models.Catalog;
using MB.Web.Services.Interfaces;

namespace MB.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateProductAsync(ProductCreateInput productCreateInput)
        {
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

            return products.Data;
        }

        public async Task<ProductViewModel> GetProductByIdAsync(int productid)
        {
            var response = await _httpClient.GetAsync($"products/{productid}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var products = await response.Content.ReadFromJsonAsync<Response<ProductViewModel>>();

            return products.Data;
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateInput productUpdateInput)
        {
            var response = await _httpClient.PutAsJsonAsync<ProductUpdateInput>("products", productUpdateInput);

            return response.IsSuccessStatusCode;
        }
    }
}
