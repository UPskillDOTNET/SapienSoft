using MammothWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Json;

namespace MammothWeb.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Register()
        {
            var token = Request.Cookies["token"];
            if (token == null)
                ViewBag.Token = false;
            else
                ViewBag.Token = true;
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
                    TempData["message"] = "New user registered";
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(registerModel);
        }

        public IActionResult Login()
        {
            var token = Request.Cookies["token"];
            if (token == null)
                ViewBag.Token = false;
            else
                ViewBag.Token = true;
            return View(new LoginModel());
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            using (var client = new HttpClient())
            {
                try
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
                } catch
                {
                    TempData["message"] = "Server connection failed";
                    return RedirectToAction("Index", "Home");
                }
            }
            //ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            TempData["message"] = "Pega lá mais um erro.";
            return View(loginModel);
        }

        public IActionResult Logout()
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddHours(-1);
            Response.Cookies.Append("token", "", cookieOptions);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile()
        {
            
            return View();
        }
    }
}
