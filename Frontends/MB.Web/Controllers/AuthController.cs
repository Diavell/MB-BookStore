﻿using MB.Web.Models;
using MB.Web.Services.Interfaces;
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
    }
}