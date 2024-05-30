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

        public async Task<IActionResult> AllUsers(string sortOrder, int? pageNumber, string searchQuery)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchQuery;
            ViewData["IdSortParm"] = sortOrder == "Id" ? "id_desc" : "Id";

            var users = await _userService.GetAllUsers();
            var queryableusers = users.AsQueryable();

            if (!String.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                queryableusers = queryableusers.Where(u =>
                    u.Id.ToString().ToLower().Contains(searchQuery) ||
                    u.UserName.ToString().ToLower().Contains(searchQuery) ||
                    u.Email.ToString().ToLower().Contains(searchQuery) ||
                    u.City.ToString().ToLower().Contains(searchQuery));
            }

            switch (sortOrder)
            {
                case "Id":
                    queryableusers = queryableusers.OrderBy(p => p.Id);
                    break;
                case "id_desc":
                    queryableusers = queryableusers.OrderByDescending(p => p.Id);
                    break;
                default:
                    queryableusers = queryableusers.OrderBy(p => p.Id);
                    break;
            }

            int pageSize = 5;
            return View(PaginatedList<UserViewModel>.Create(queryableusers, pageNumber ?? 1, pageSize));
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

            if (User.Identity.Name == "Admin")
            {
                return RedirectToAction(nameof(AllUsers));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteUserAsync(id);

            return RedirectToAction(nameof(AllUsers));
        }
    }
}
