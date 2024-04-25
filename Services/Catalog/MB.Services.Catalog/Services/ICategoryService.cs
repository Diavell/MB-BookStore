using MB.Services.Catalog.Dtos;
using MB.Shared.Dtos;

namespace MB.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<Response<CategoryDto>> GetByIdAsync(string id);
        Task<Response<NoContent>> UpdateAsync(CategoryDto categoryDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
