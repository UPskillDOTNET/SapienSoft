using MammothWeb.Globals;
using MammothWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MammothWeb.Controllers
{
    public class ReservationsController : Controller
    {
        // GET: ReservationsController
        public ActionResult Index()
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

        // GET: ReservationsController/Details/5
        public ActionResult Details(int id)
        {
            Reservation reservation = new Reservation();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/reservations/");
                var response = client.GetAsync(id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<Reservation>();
                    read.Wait();
                    reservation = read.Result;
                }
                else
                {
                    //erro
                    reservation = null;
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }
                return View(reservation);
            }
        }

        // GET: ReservationsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReservationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reservation reservation, int idPark)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/");
                var response = client.PostAsJsonAsync("reservations", reservation);
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<Reservation>();
                    read.Wait();
                    reservation = read.Result;
                }
                else
                {
                    //erro
                    reservation = null;
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }
                return RedirectToAction("Index", "Reservations");
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

        public async Task<ActionResult> Delete(string id)
        {

            if (id == null)
            {
                return BadRequest("Not found");
            }
            Reservation reservation = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/parks/");
                var result = await client.GetAsync(id);

                if (result.IsSuccessStatusCode)
                {
                    reservation = await result.Content.ReadAsAsync<Reservation>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(reservation);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/");
                var response = await client.DeleteAsync($"reservations/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Reservations");
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
