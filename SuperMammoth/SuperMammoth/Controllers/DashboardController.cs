using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMammoth.Models;
using System.Net.Http;

namespace SuperMammoth.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Charts
        public ActionResult Charts()
        {
            List<Transaction> transList = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/");
                var response = client.GetAsync("transactions");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<List<Transaction>>();
                    read.Wait();
                    transList = read.Result;
                }
                else
                {
                    //erro
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }
                return View(transList);
            }
        }
        // GET: Charts/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Charts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Charts/Create
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

        // GET: Charts/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Charts/Edit/5
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

        // GET: Charts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Charts/Delete/5
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
