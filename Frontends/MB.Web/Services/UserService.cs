using IdentityModel.Client;
using MB.Shared.Dtos;
using MB.Web.Models;
using MB.Web.Models.Catalog;
using MB.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json;

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
        
        public async Task<UserViewModel> GetUsersById(string userId)
        {
            return await _httpClient.GetFromJsonAsync<UserViewModel>($"api/user/getuserbyid/{userId}");
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            var response = await _httpClient.GetFromJsonAsync<List<UserViewModel>>("api/user/getallusers");

            response.Remove(response.Find(x => x.Id == "4e078494-4486-4a38-987c-87786a5ee3d5"));

            return response;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var response = await _httpClient.DeleteAsync($"api/user/delete/{userId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUserAsync(UserViewModel userViewModel)
        {
            var response = await _httpClient.PutAsJsonAsync<UserViewModel>("api/user/update", userViewModel);

            return response.IsSuccessStatusCode;
        }
    }
}
