using iParkMedusa.Models;
using iParkMedusa.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace iParkMedusa.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {
            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
        {
            var result = await _userService.GetTokenAsync(model);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            var result = await _userService.AddRoleAsync(model);
            return Ok(result);
        }

        // Added to allow for Password Changes
        [Authorize(Roles = "Administrator, Moderator, User")]
        [HttpPut("password")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var userName = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);

            if (User.IsInRole("Administrator") || (User.IsInRole("User") && user.Email == model.Email))
            {
                var result = await _userService.ChangePasswordAsync(model);
                return Ok(result);
            }
            return Unauthorized("No, no, no...");
        }

        [Authorize(Roles = "Administrator, Moderator, User")]
        [HttpGet("info")]
        public IActionResult GetUserInfo()
        {
            var userName = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            return Ok(user);
        }

        [Authorize(Roles = "Administrator")]
        [Route("~/api/users/ById/{id}")]
        [HttpGet]
        public IActionResult GetUserById(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            string userName = user.UserName;
            return Ok(userName);
        }
        [Authorize(Roles = "Administrator, Moderator, User")]
        [HttpPut("edit")]
        public async Task<IActionResult> ChangeUserInfoAsync(RegisterModel model)
        {
            var userName = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);

            var result = await _userService.EditUser(model, user);
            return Ok(result);
        }
        [Authorize(Roles = "Administrator, Moderator, User")]
        [HttpGet("getId/{userName}")]
        public string GetUserIdByUserName(string userName)
        {
            
           
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            var id = user.Id.ToString();
            
            return id;
        }

    }
}
