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
    public class ReservationsController : Controller
    {
        // GET: ReservationsController
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Reservation> reservation = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44398/api/");
                    var response = client.GetAsync("reservations");
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var read = result.Content.ReadAsAsync<IList<Reservation>>();
                        read.Wait();
                        reservation = read.Result;
                    }
                    else
                    {
                        //erro
                        reservation = Enumerable.Empty<Reservation>();
                        ModelState.AddModelError(string.Empty, "Server error occured");
                    }
                    return View(reservation);
                }
            }
            catch
            {
                TempData["message"] = "Server connection failed";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: ReservationsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReservationsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReservationsController/Create
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

        // GET: ReservationsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReservationsController/Edit/5
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

        // GET: ReservationsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReservationsController/Delete/5
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
