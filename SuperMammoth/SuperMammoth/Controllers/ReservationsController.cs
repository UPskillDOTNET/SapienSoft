using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMammoth.Globals;
using SuperMammoth.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SuperMammoth.Controllers
{
    public class ReservationsController : BaseController
    {

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ReservationDTOModel reservationModel)
        {

            using (var client = new HttpClient())
            {
                IEnumerable<ReservationDTOModel> reservation = new List<ReservationDTOModel>();
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
                    var read = result.Content.ReadAsAsync<IList<ReservationDTOModel>>();
                    read.Wait();
                    reservation = read.Result;
                }
                else
                {
                    //erro
                    reservation = Enumerable.Empty<ReservationDTOModel>();
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }
                if(reservation.Count() == 0)
                {
                    TempData["message"] = " No available reservations were found matching the search criteria.";
                }
                return View("ReservationList", reservation);
            }
        }

        public ActionResult ReservationList(List<ReservationDTOModel> reservation)
        {
            return View(reservation);
        }

        public ActionResult Details(ReservationDTOModel reservation)
        {
            return View(reservation);
        }
        public ActionResult SubRentalList()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SubRentalList   (ReservationModel reservation)
        {
            using (var client = new HttpClient())
            {
                IEnumerable<ReservationModel> reservationList = new List<ReservationModel>();
                client.BaseAddress = new Uri("https://localhost:44398/api/"); // MedusaAPI

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //Token
                var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
                var token = userSession.Token;

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = client.GetAsync($"reservations/available/rent?start={reservation.Start.ToString("o")}&end={reservation.End.ToString("o")}"); // &latitude={reservationModel.Latitude}&longitude={reservationModel.Longitude}
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<IList<ReservationModel>>();
                    read.Wait();
                    reservationList = read.Result;
                }
                else
                {
                    //erro
                    reservationList = Enumerable.Empty<ReservationModel>();
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }
                if (reservationList.Count() == 0)
                {
                    TempData["message"] = " No available reservations were found matching the search criteria.";
                }
                return View("SubRentList", reservationList);
            }
        }

        public ActionResult SubRent(ReservationModel reservation)
        {
            var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
            var token = userSession.Token;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44398/api/user/"); // MedusaAPI

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //Token


                //Getting the current user Id
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = client.GetAsync("getId/" + userSession.UserName);
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<string>();
                    read.Wait();
                    var Id = read.Result;
                    //Compare current user to reservation user
                    if (Id == reservation.UserId)
                    {
                        TempData["message"] = " You cannot sub-rent a reservation you already own.";
                        return RedirectToAction("SubRentalList");
                    }
                    
                        
                        client.BaseAddress = new Uri("https://localhost:44398/api/reservations/"); // MedusaAPI

                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        //Token



                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                        var response2 = client.PostAsJsonAsync("rented/" + reservation.Id, "");
                        response2.Wait();

                        var result2 = response2.Result;
                        if (result2.IsSuccessStatusCode)
                        {
                            var read2 = result2.Content.ReadAsAsync<ReservationModel>();
                            read2.Wait();
                            var NewReservation = read2.Result;
                            TempData["message"] = " Reservation has been made.";
                            return RedirectToAction("ReservationDetails", NewReservation);
                        }
                    else
                    {
                        //erro
                        ModelState.AddModelError(string.Empty, "Server error occured");
                        return View();
                    }


                }
                else
                {
                    //erro
                    ModelState.AddModelError(string.Empty, "Server error occured");
                    return View();
                }
                
            }
        }
            public ActionResult MakeReservation(ReservationDTOModel reservationModel)
        {
                IEnumerable<Park> parks = GetParks();
                int parkId;
                foreach (Park p in parks)
                {
                    if (p.Name == reservationModel.ParkName)
                    {
                        parkId = p.Id;

                        using (var client = new HttpClient())
                        {

                            client.BaseAddress = new Uri("https://localhost:44398/api/reservations/"); // MedusaAPI

                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            //Token
                            var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
                            var token = userSession.Token;

                            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                            var response = client.PostAsJsonAsync(parkId.ToString(), reservationModel);
                            response.Wait();

                            var result = response.Result;
                            if (result.IsSuccessStatusCode)
                            {
                                var read = result.Content.ReadAsAsync<ReservationModel>();
                                read.Wait();
                                var reservation = read.Result;
                                TempData["message"] = " Reservation has been made.";
                                return RedirectToAction("GetReservationByUser");
                            }
                            else if (result.ReasonPhrase.Contains("Payment Required"))
                            {
                                TempData["message"] = " Insufficient funds.";
                                return RedirectToAction("Create", "Transactions");                                
                            }
                        else
                            {
                                //erro
                                ModelState.AddModelError(string.Empty, "Server error occured");
                                TempData["message"] = " Sorry. Something went wrong.";
                                return RedirectToAction("Index", "Home");
                        }
                            
                        }
                    }
                }
           
            return BadRequest("No park was found. Contact support");
        }

        public IEnumerable<Park> GetParks()
        {
            IEnumerable<Park> parks = null;
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
                    parks = read.Result;
                }
                else
                {
                    //erro
                    parks = Enumerable.Empty<Park>();
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }
                return parks;
            }
        }

        public ActionResult<IEnumerable<ReservationModel>> GetReservationByUser(string sortOrder)
        {
            using (var client = new HttpClient())
            {
                IEnumerable<ReservationModel> userReservations = new List<ReservationModel>();
                client.BaseAddress = new Uri("https://localhost:44398/api/reservations/"); // MedusaAPI

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //Token
                var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
                var token = userSession.Token;

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = client.GetAsync("user");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<IList<ReservationModel>>();
                    read.Wait();
                    userReservations = read.Result;
                }
                else
                {
                    //erro
                    userReservations = Enumerable.Empty<ReservationModel>();
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }

                ViewData["StarSortParm"] = sortOrder == "start" ? "start_desc" : "start";
                ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_asc" : "";
                ViewData["EndSortParm"] = sortOrder == "end" ? "end_desc" : "end";


                var myReservations = userReservations.AsQueryable();

                switch (sortOrder)
                {
                    case "start_desc":
                        myReservations = myReservations.OrderByDescending(r => r.Start);
                        break;

                    case "date_asc":
                        myReservations = myReservations.OrderBy(r => r.DateCreated);
                        break;

                    case "end_desc":
                        myReservations = myReservations.OrderByDescending(r => r.End);
                        break;

                    case "start":
                        myReservations = myReservations.OrderBy(r => r.Start);
                        break;

                    case "end":
                        myReservations = myReservations.OrderBy(r => r.End);
                        break;

                    default:
                        myReservations = myReservations.OrderByDescending(r => r.DateCreated);
                        break;
                }

                return View("UserReservations", myReservations);
            }
        }

        public ActionResult UserReservations(IList<ReservationModel> reservation)
        { 

            return View(reservation);

        }
        public async Task<ActionResult> MakeAvailableToRent(string id)
        {
            if (id == null)
            {
                return BadRequest("Not found");
            }
            ReservationModel reservation = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/reservations/");
                var result = await client.GetAsync(id.ToString());

                if (result.IsSuccessStatusCode)
                {
                    reservation = await result.Content.ReadAsAsync<ReservationModel>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(reservation);
        }
        [HttpPost]
        public ActionResult MakeAvailableToRent(ReservationModel reservation)
        {
            
                using (var client = new HttpClient())
                 {

                        client.BaseAddress = new Uri("https://localhost:44398/api/reservations/"); // MedusaAPI

                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        //Token
                        var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
                        var token = userSession.Token;

                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                        var response = client.PutAsJsonAsync("rent?id="+ reservation.Id +"&rentValue=" + reservation.RentValue, "");
                        response.Wait();

                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var read = result.Content.ReadAsAsync<ReservationModel>();
                            read.Wait();
                            var newreservation = read.Result;
                            TempData["message"] = " Reservation is now available to sub-rent.";
                            return RedirectToAction("GetReservationByUser", "Reservations");
                         }
                        else
                        {
                            //erro
                            ModelState.AddModelError(string.Empty, "Server error occured");
                            return View();
                        }
                        
                }
            
           
        
        }
        public IActionResult ReservationDetails (int id)
        {
            ReservationModel reservation = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/reservations/");
                var response = client.GetAsync(id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<ReservationModel>();
                    read.Wait();
                    reservation = read.Result;

                    
                    
                    ViewBag.url = "https://maps.google.com/maps?q=" + reservation.Latitude.ToString().Replace(',', '.') + "," + reservation.Longitude.ToString().Replace(',', '.') + "&t=&z=15&ie=UTF8&iwloc=&output=embed";

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
        public IActionResult ReSend (int id)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44398/api/reservations/"); // MedusaAPI

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //Token
                var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
                var token = userSession.Token;

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = client.GetAsync("resend/"+id);
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["message"] = " Reservation e-mail has been resent.";
                    return RedirectToAction("GetReservationByUser");
                }
                else
                {
                    //erro
                    ModelState.AddModelError(string.Empty, "Server error occured");
                    return View();
                }

            }
        }

        public ActionResult Delete(ReservationModel reservation)
        {
            return View(reservation);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult CancelReservation(int id)
        {

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44398/api/reservations/"); // MedusaAPI

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //Token
                var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
                var token = userSession.Token;

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = client.DeleteAsync(id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["message"] = " Reservation has been cancelled.";
                    return RedirectToAction("GetReservationByUser");
                }
                else
                {
                    //erro
                    ModelState.AddModelError(string.Empty, "Server error occured");
                    return View();
                }

            }



        }
    }
}
