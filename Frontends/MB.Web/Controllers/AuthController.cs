using MB.Web.Models;
using MB.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace MB.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SigninInput signinInput)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _identityService.SignIn(signinInput);

            if (!response.IsSuccess)
            {
                response.Error.ForEach(error => ModelState.AddModelError(string.Empty, error));
                return View();
            }

            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult AdminSignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminSignIn(SigninInput signinInput)
        {
            string AdminEmail = "erdemc@hotmail.com";
            string AdminPassword = "Password12*";
            string Error = "Invalid username or password";

            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _identityService.SignIn(signinInput);

            if (signinInput.Email != AdminEmail || signinInput.Password != AdminPassword)
            {
                ModelState.AddModelError(string.Empty, Error);
                return View();
            }

            if (!response.IsSuccess)
            {
                response.Error.ForEach(error => ModelState.AddModelError(string.Empty, error));
                return View();
            }

            return RedirectToAction(nameof(Index), "Admin");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterInput registerInput)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Enter all fields correctly");
                return View();
            }

            if (registerInput.Password != registerInput.RePassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match");
                return View();
            }

            var response = await _identityService.Register(registerInput);

            if (!response.IsSuccess)
            {
                response.Error.ForEach(error => ModelState.AddModelError(string.Empty, error));
                return View();
            }

            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            await _identityService.RevokeRefreshToken();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
