using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperMammoth.Globals;
using SuperMammoth.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMammoth.Controllers
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
            // Check for session and retrieve role
            var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
            if (userSession == null)
            {
                ViewBag.Role = "";
            }
            else
            {
                if (userSession.Roles.Contains("Administrator") == true)
                    ViewBag.Role = "Administrator";
                else if (userSession.Roles.Contains("User") == true)
                    ViewBag.Role = "User";
            }
               
            return View();
        }

        public IActionResult Privacy()
        {
            // Check for session and retrieve role
            var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
            if (userSession == null)
            {
                ViewBag.Role = "";
            }
            else
            {
                if (userSession.Roles.Contains("Administrator") == true)
                    ViewBag.Role = "Administrator";
                else if (userSession.Roles.Contains("User") == true)
                    ViewBag.Role = "User";
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
