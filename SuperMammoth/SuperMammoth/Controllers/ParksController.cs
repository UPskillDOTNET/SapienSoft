using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using SuperMammoth.Models;
using SuperMammoth.Globals;

namespace SuperMammoth.Controllers
{
    public class ParksController : BaseController
    {
        // GET: ParksController
        public ActionResult Index()
        {
            if (!HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession").Roles.Contains("Administrator"))
                return RedirectToAction("Index", "Home");

            IEnumerable<Park> park = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/");
                var response = client.GetAsync("parks");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<IList<Park>>();
                    read.Wait();
                    park = read.Result;
                }
                else
                {
                    //erro
                    park = Enumerable.Empty<Park>();
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }
                return View(park);
            }
        }

        // GET: ParksController/Details/5
        public ActionResult Details(int id)
        {
            if (!HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession").Roles.Contains("Administrator"))
                return RedirectToAction("Index", "Home");

                Park park = new Park();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/parks/");
                var response = client.GetAsync(id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<Park>();
                    read.Wait();
                    park = read.Result;
                }
                else
                {
                    //erro
                    park = null;
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }
                return View(park);
            }
        }

        public ActionResult Create()
        {
            if (!HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession").Roles.Contains("Administrator"))
                return RedirectToAction("Index", "Home");
            return View();
        }


        // POST: ParksController/Create
        [HttpPost]
        public ActionResult Create(Park park)
        {
            if (!HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession").Roles.Contains("Administrator"))
                return RedirectToAction("Index", "Home");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/");
                var response = client.PostAsJsonAsync("parks", park);
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<Park>();
                    read.Wait();
                    park = read.Result;
                }
                else
                {
                    //erro
                    park = null;
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }
                return RedirectToAction("Index", "Parks");
            }

        }

        public async Task<ActionResult> Edit(string id)
        {
            if (!HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession").Roles.Contains("Administrator"))
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return BadRequest("Not found");
            }
            Park park = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/parks/");
                var result = await client.GetAsync(id.ToString());

                if (result.IsSuccessStatusCode)
                {
                    park = await result.Content.ReadAsAsync<Park>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(park);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Edit([Bind("Id", "Name", "Uri")] Park park)
        {
            if (!HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession").Roles.Contains("Administrator"))
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44398/api/");
                    var response = await client.PutAsJsonAsync($"parks/{park.Id}", park);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Parks");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
                return RedirectToAction("Index", "Parks");
            }
            return View(park);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (!HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession").Roles.Contains("Administrator"))
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return BadRequest("Not found");
            }
            Park park = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/parks/");
                var result = await client.GetAsync(id);

                if (result.IsSuccessStatusCode)
                {
                    park = await result.Content.ReadAsAsync<Park>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(park);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (!HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession").Roles.Contains("Administrator"))
                return RedirectToAction("Index", "Home");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/");
                var response = await client.DeleteAsync($"parks/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Parks");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return View();
        }
    }
}
