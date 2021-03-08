﻿using Microsoft.AspNetCore.Http;
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
                        }
                        else
                        {
                            //erro
                            ModelState.AddModelError(string.Empty, "Server error occured");
                        }
                        return View();
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

        public ActionResult<IEnumerable<ReservationModel>> GetReservationByUser()
        {
            using (var client = new HttpClient())
            {
                IEnumerable<ReservationModel> userReservations = new List<ReservationModel>();
                client.BaseAddress = new Uri("https://localhost:44398/api/reservations/user/"); // MedusaAPI

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //Token
                var userSession = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");
                var token = userSession.Token;

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = client.GetAsync("byid");
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
                return View("UserReservations", userReservations);
            }
        }

        public ActionResult UserReservations(List<ReservationModel> reservation)
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
                        var response = client.PutAsJsonAsync("rent?id="+ reservation.Id +"&rentValue =" + reservation.RentValue, "");
                        response.Wait();

                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var read = result.Content.ReadAsAsync<ReservationModel>();
                            read.Wait();
                            var newreservation = read.Result;
                             return View("ReservationList");
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
