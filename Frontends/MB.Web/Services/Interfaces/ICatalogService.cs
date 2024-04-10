using MB.Web.Models.Catalog;

namespace MB.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<ProductViewModel>> GetAllProductsAsync();

        Task<List<CategoryViewModel>> GetAllCategoriesAsync();

        Task<ProductViewModel> GetProductByIdAsync(string productid);

        Task<bool> CreateProductAsync(ProductCreateInput productCreateInput);

        Task<bool> UpdateProductAsync(ProductUpdateInput productUpdateInput);

        Task<bool> DeleteProductAsync(string productId);
    }
}
