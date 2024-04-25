using MB.Web.Models.Catalog;

namespace MB.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<ProductViewModel>> GetAllProductsAsync();

        Task<List<CategoryViewModel>> GetAllCategoriesAsync();

        Task<ProductViewModel> GetProductByIdAsync(string productid);

        Task<CategoryViewModel> GetCategoryByIdAsync(string categoryId);

        Task<bool> CreateProductAsync(ProductCreateInput productCreateInput);

        Task<bool> CreateCategoryAsync(CategoryCreateInput categoryCreateInput);

        Task<bool> UpdateProductAsync(ProductUpdateInput productUpdateInput);

        Task<bool> UpdateCategoryAsync(CategoryUpdateInput categoryUpdateInput);

        Task<bool> DeleteProductAsync(string productId);

        Task<bool> DeleteCategoryAsync(string categoryId);
    }
}
