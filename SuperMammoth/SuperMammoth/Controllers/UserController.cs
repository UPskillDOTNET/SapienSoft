using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMammoth.Globals;
using SuperMammoth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SuperMammoth.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Register()
        {

            return View();
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
                    TempData["message"] = "Registation successful!";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult Login()
        {

            return View();
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
                        var authenticationModel = content.Result;
                        HttpContext.Session.SetObjectAsJson("UserSession", authenticationModel);
                        
                        TempData["message"] = authenticationModel.Message;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["message"] = "Login failed!";
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch
                {
                    TempData["message"] = "Server connection failed";
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public IActionResult Profile()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:44398/api/");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
                    var token = userSession.Token;
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    // token

                    var postTask = client.GetAsync("user/info");
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var content = result.Content.ReadFromJsonAsync<RegisterModel>();
                        content.Wait();
                        var userModel = content.Result;
                        return View(userModel);
                    }
                    else
                    {
                        TempData["message"] = "Error!";
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch
                {
                    TempData["message"] = "Server connection failed";
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:44398/api/");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
                    var token = userSession.Token;
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    // token

                    var postTask = client.GetAsync("user/info");
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var content = result.Content.ReadFromJsonAsync<RegisterModel>();
                        content.Wait();
                        var userModel = content.Result;
                        return View(userModel);
                    }
                    else
                    {
                        TempData["message"] = "Error!";
                        return RedirectToAction("Profile", "User");
                    }
                }
                catch
                {
                    TempData["message"] = "Server connection failed";
                    return RedirectToAction("Index", "Home");
                }
            }
        }



            [HttpPost]
            [ValidateAntiForgeryToken]

            public async Task<ActionResult> Edit(RegisterModel register)
            {
                using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44398/api/user/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
                var token = userSession.Token;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await client.PutAsJsonAsync("edit", register);
                        if (response.IsSuccessStatusCode)
                        {
                            var content = response.Content.ReadFromJsonAsync<AuthenticationModel>();
                            content.Wait();
                            var userModel = content.Result;
                            
                            HttpContext.Session.SetObjectAsJson("UserSession", userModel);
                             return RedirectToAction("Profile", "User");
                        }
                        else
                        {
                        TempData["message"] = "Error!";
                        return RedirectToAction("Index", "Home");
                         }
                   }
               
            }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ChangePassword (ChangePasswordModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/user/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
                var token = userSession.Token;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await client.PutAsJsonAsync("password", model);
                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("Profile", "User");
                }
                else
                {
                    TempData["message"] = "Error!";
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        
    }
}
