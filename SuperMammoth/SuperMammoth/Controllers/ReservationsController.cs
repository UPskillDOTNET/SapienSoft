using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMammoth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SuperMammoth.Controllers
{
    public class ReservationsController : Controller
    {

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult ChooseDate(DateTime start, DateTime end)
        {
            IEnumerable<Reservation> reservation = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/Reservations/Available?");
                var response = client.GetAsync($"start={start}&end={end}");
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

        
    }
}
