using MB.Web.Models;
using MB.Web.Models.Catalog;

namespace MB.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUsers();
        Task<List<UserViewModel>> GetAllUsers();
        Task<UserViewModel> GetUsersById(string userId);
        Task<bool> UpdateUserAsync(UserViewModel userViewModel);
        Task<bool> DeleteUserAsync(string userId);
    }
}
