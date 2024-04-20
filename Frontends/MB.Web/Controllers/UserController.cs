using MB.Web.Models;
using MB.Web.Models.Catalog;
using MB.Web.Services;
using MB.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MB.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetUsers());
        }

        public async Task<IActionResult> AllUsers()
        {
            return View(await _userService.GetAllUsers());
        }

        public async Task<IActionResult> Update(string id)
        {
            var user = await _userService.GetUsersById(id);

            if (user == null)
            {
                //TO DO alert gösterilecek
                return RedirectToAction(nameof(Index));
            }

            UserViewModel userViewModel = new()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                City = user.City
            };

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _userService.UpdateUserAsync(userViewModel);

            return RedirectToAction(nameof(AllUsers));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteUserAsync(id);

            return RedirectToAction(nameof(AllUsers));
        }
    }
}
