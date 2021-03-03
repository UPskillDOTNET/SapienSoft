using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMammoth.Globals;
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

        [HttpPost]
        public ActionResult Create(ReservationModel reservationModel)
        {

            using (var client = new HttpClient())
            {
                IEnumerable<ReservationModel> reservation = new List<ReservationModel>();
                client.BaseAddress = new Uri("https://localhost:44398/api/"); // MedusaAPI

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //Token
                var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
                var token = userSession.Token;

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = client.GetAsync($"reservations/available?start={reservationModel.Start.ToString("o")}&end={reservationModel.End.ToString("o")}"); // &latitude={reservationModel.Latitude}&longitude={reservationModel.Longitude}
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<IList<ReservationModel>>();
                    read.Wait();
                    reservation = read.Result;
                }
                else
                {
                    //erro
                    reservation = Enumerable.Empty<ReservationModel>();
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }
                return View("ReservationList", reservation);
            }
        }

        public ActionResult ReservationList(List<ReservationModel> reservation)
        {
            return View(reservation);
        }


    }
}
