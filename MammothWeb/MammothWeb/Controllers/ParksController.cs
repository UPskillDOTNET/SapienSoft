using MammothWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MammothWeb.Controllers

{
    public class ParksController : Controller
    {
        // GET: ParksController
        public ActionResult Index()
        {
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
            return View();
        }

        // GET: ParksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ParksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ParksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ParksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ParksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
