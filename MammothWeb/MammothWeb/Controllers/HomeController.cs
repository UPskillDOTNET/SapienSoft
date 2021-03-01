using MammothWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MammothWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var token = Request.Cookies["token"];
            if (token == null)
            {
                ViewBag.Token = false;
                TempData["role"] = null;
            }
            else
            {
                // GetUserRole
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri("https://localhost:44398/api/");
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                        var postTask = client.GetAsync("user/role");
                        postTask.Wait();
                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var content = result.Content.ReadAsStringAsync();
                            content.Wait();
                            var role = content.Result;
                            TempData["role"] = content.Result;
                        }
                        else
                            TempData["role"] = null;
                    }
                    catch
                    {
                        TempData["role"] = null;
                    }
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
