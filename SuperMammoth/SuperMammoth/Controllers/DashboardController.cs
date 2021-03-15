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
using Microsoft.AspNetCore.Authorization;

namespace SuperMammoth.Controllers
{
    public class DashboardController : BaseController
    {
        public ActionResult Index()
        {
            if (!HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession").Roles.Contains("Administrator"))
                return RedirectToAction("Index", "Home");
            using (var client = new HttpClient())
            {
                //////////////////////////////////////Transactions/Column///////////////////////////////////
                var temp = HttpContext.Session.GetObjectFromJson<AuthenticationModel>("UserSession");

                client.BaseAddress = new Uri("https://localhost:44398/api/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + temp.Token);
                var Tresponse = client.GetAsync("transactions");
                Tresponse.Wait();

                var Tresult = Tresponse.Result;
                if (Tresult.IsSuccessStatusCode)
                {
                    var Tread = Tresult.Content.ReadFromJsonAsync<IList<Transaction>>();
                    Tread.Wait();
                    var transactionlist = Tread.Result;
                    var transaction = transactionlist.AsQueryable();
                    var addValue = transaction.Where(t => t.TransactionTypeId.Equals(2)).Sum(t => t.Value);
                    var cancelValue = transaction.Where(t => t.TransactionTypeId.Equals(3)).Sum(t => t.Value);
                    var spentValue = (transaction.Where(t => t.TransactionTypeId.Equals(1)).Sum(t => t.Value)) * -1;
                    List<DataPoints> dataPoints = new List<DataPoints>{
                                                    new DataPoints("Funds Added", addValue),
                                                    new DataPoints("Funds Spent", spentValue),
                                                    new DataPoints("Canceled Reservation Funds", cancelValue),
                                                   };

                    ViewBag.columnDataPoints = Newtonsoft.Json.JsonConvert.SerializeObject(dataPoints);
                    ViewBag.totalAdd = addValue;
                    ViewBag.totalSpent = spentValue;
                }
                //////////////////////////////////////Reservations/Area///////////////////////////////////
                var Rresponse = client.GetAsync("reservations");
                Rresponse.Wait();

                var Rresult = Rresponse.Result;
                if (Rresult.IsSuccessStatusCode)
                {
                    var Rread = Rresult.Content.ReadFromJsonAsync<IList<ReservationModel>>();
                    Rread.Wait();
                    var reservationlist = Rread.Result;
                    var reservations = reservationlist.AsQueryable();

                    List<AreaDataPoints> createdAtmonth = new List<AreaDataPoints>();
                    List<AreaDataPoints> startAtmonth = new List<AreaDataPoints>();
                    List<AreaDataPoints> endAtmonth = new List<AreaDataPoints>();
                    DateTime currentYear = DateTime.Now;

                    for (int i = 1; i <= 12; i++)
                    {
                        var Createdcount = reservations.Where(r => r.DateCreated.Month == i).Where(r => r.DateCreated.Year == currentYear.Year).Count();
                        var Startedcount = reservations.Where(r => r.Start.Month == i).Where(r => r.Start.Year == currentYear.Year).Count();
                        var Endedcount = reservations.Where(r => r.End.Month == i).Where(r => r.End.Year == currentYear.Year).Count();



                        AreaDataPoints created = new AreaDataPoints(i, Createdcount);
                        createdAtmonth.Add(created);
                        AreaDataPoints started = new AreaDataPoints(i, Startedcount);
                        startAtmonth.Add(started);
                        AreaDataPoints ended = new AreaDataPoints(i, Endedcount);
                        endAtmonth.Add(ended);

                    }

                    ViewBag.createAtmonth = Newtonsoft.Json.JsonConvert.SerializeObject(createdAtmonth);
                    ViewBag.startAtmonth = Newtonsoft.Json.JsonConvert.SerializeObject(startAtmonth);
                    ViewBag.endAtmonth = Newtonsoft.Json.JsonConvert.SerializeObject(endAtmonth);
                    ViewBag.activeReservations = reservations.Where(r => r.End > DateTime.Now).Count();
                    ViewBag.totalReservations = reservations.Count();

                    //////////////////////////////////////Reservations/Pie///////////////////////////////////

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
                        ViewBag.pieDataPoints = Newtonsoft.Json.JsonConvert.SerializeObject(dataPoints);
                        ViewBag.totalParks = parks.Count();
                    }

                }
            }






            return View();
        }
    
       
    }
}
