using MB.Web.Models;
using MB.Web.Services.Interfaces;

namespace MB.Web.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserViewModel> GetUsers()
        {
            return await _httpClient.GetFromJsonAsync<UserViewModel>("api/user/getuser");
        }
    }
}
