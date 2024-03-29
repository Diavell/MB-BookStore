using MB.Web.Models;

namespace MB.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUsers();
    }
}
