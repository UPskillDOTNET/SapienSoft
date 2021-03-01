using MammothWeb.Globals;
using MammothWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MammothWeb.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Register()
        {

            return View(new RegisterModel());
        }

        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var postTask = client.PostAsJsonAsync("user/register", registerModel);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();
                    content.Wait();
                    string message = content.Result;
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(registerModel);
        }

        public IActionResult Login()
        {

            return View(new LoginModel());
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var postTask = client.PostAsJsonAsync("user/token", loginModel);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadFromJsonAsync<AuthenticationModel>();
                    content.Wait();
                    if (content.Result.Token != null)
                    {
                        CookieOptions cookieOptions = new CookieOptions();
                        cookieOptions.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Append("token", content.Result.Token, cookieOptions);
                        TempData["message"] = "Login successful";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["message"] = "Login failed";
                        return RedirectToAction("Index", "Home");
                    } 
                }
            }
            //ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            TempData["message"] = "Server error.";
            return View(loginModel);
        }

        public IActionResult Logout()
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddHours(-1);
            Response.Cookies.Append("token", "", cookieOptions);
            return RedirectToAction("Index", "Home");
        }
    }
}
