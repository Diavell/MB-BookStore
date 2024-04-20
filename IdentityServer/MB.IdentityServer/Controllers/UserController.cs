using IdentityServer4.Hosting.LocalApiAuthentication;
using MB.IdentityServer.Dtos;
using MB.IdentityServer.Models;
using MB.IdentityServer.Services;
using MB.Shared.ControllerBases;
using MB.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MB.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignupDto signupDto)
        {
            var user = new ApplicationUser
            {
                UserName = signupDto.UserName,
                Email = signupDto.Email,
                City = signupDto.City
            };

            var result = await _userManager.CreateAsync(user, signupDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x=>x.Description).ToList(),400));
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

            if (userIdClaim == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(userIdClaim.Value);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(new {user.Id, user.UserName, user.Email, user.City});
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            if (users == null || users.Count == 0)
            {
                return NoContent();
            }

            var userList = users.Select(user => new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.City

            }).ToList();

            return Ok(userList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(new { user.Id, user.UserName, user.Email, user.City });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id);

            if (user == null)
            {
                return BadRequest();
            }

            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.City = userDto.City;

            var response = await _userManager.UpdateAsync(user);

            if (!response.Succeeded)
            {
                return BadRequest();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return BadRequest();
            }

            var response = await _userManager.DeleteAsync(user);

            if (!response.Succeeded)
            {
                return BadRequest();
            }

            return Ok(response);
        }
    }
}
