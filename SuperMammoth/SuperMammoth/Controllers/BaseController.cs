using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SuperMammoth.Globals;
using SuperMammoth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMammoth.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession") == null)
            {
                TempData["message"] = "Please login.";
                filterContext.HttpContext.Response.Redirect("/User/Login");
            }
        }
    }
}
