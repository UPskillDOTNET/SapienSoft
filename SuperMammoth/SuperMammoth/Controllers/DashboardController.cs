using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMammoth.Models;
using System.Net.Http;
using SuperMammoth.Globals;
using System.Net.Http.Json;

namespace SuperMammoth.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Charts
        public ActionResult Charts()
        {
                return View();
        }
        public ActionResult Column()
        {
            using (var client = new HttpClient())
            {
                var temp = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");

                client.BaseAddress = new Uri("https://localhost:44398/api/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + temp.Token);
                var response = client.GetAsync("transactions");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadFromJsonAsync<IList<Transaction>>();
                    read.Wait();
                     var transactionlist = read.Result;
                    var transaction = transactionlist.AsQueryable();
                    var addValue = transaction.Where(t => t.TransactionTypeId.Equals(2)).Sum(t => t.Value);
                    var cancelValue = transaction.Where(t => t.TransactionTypeId.Equals(3)).Sum(t => t.Value);
                    var spentValue = (transaction.Where(t => t.TransactionTypeId.Equals(1)).Sum(t => t.Value))*-1;
                    List<DataPoints> dataPoints = new List<DataPoints>{
                                                    new DataPoints("Funds Added", addValue),
                                                    new DataPoints("Funds Spent", spentValue),
                                                    new DataPoints("Canceled Reservation Funds", cancelValue),
                                                   };

                    ViewBag.DataPoints = Newtonsoft.Json.JsonConvert.SerializeObject(dataPoints);

                }
            }
                return View();
        }
        public ActionResult Bar()
        {
            return View();
        }
        public ActionResult Line()
        {
            return View();
        }
        public ActionResult Area()
        {
            return View();
        }


        public ActionResult Pie()
        {
            using (var client = new HttpClient())
            {
                var temp = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");

                client.BaseAddress = new Uri("https://localhost:44398/api/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + temp.Token);
                var response = client.GetAsync("reservations");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadFromJsonAsync<IList<ReservationModel>>();
                    read.Wait();
                    var reservationlist = read.Result;
                    var reservations = reservationlist.AsQueryable();


                    var response2 = client.GetAsync("parks");
                    response2.Wait();

                    var result2 = response2.Result;
                    if (result2.IsSuccessStatusCode)
                    {
                        var read2 = result2.Content.ReadFromJsonAsync<IList<Park>>();
                        read2.Wait();
                        var parkslist = read2.Result;
                        var parks = parkslist.AsQueryable();

                        List<DataPoints> dataPoints = new List<DataPoints>();
                        
                        foreach (var item in parks)
                        {
                            var parkName = item.Name;
                            var totalReservations = reservations.Where(r => r.ParkId.Equals(item.Id)).Count();
                            var data = new DataPoints(parkName, totalReservations);
                            dataPoints.Add(data);
                        }
                        ViewBag.DataPoints = Newtonsoft.Json.JsonConvert.SerializeObject(dataPoints);
                    }
                }    

            }
 
            return View();
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
