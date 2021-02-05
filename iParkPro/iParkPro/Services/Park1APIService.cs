using iParkPro.Entities;
using iParkPro.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace iParkPro.Services
{
    public class Park1APIService : IParkService
    {
        private readonly string username = "sapiensoft@sapiensoft.com";
        private readonly string password = "SapienSoft123!";

        public async Task<List<Reservation>> GetAvailable(DateTime start, DateTime end, string userId)
        {




            List<Reservation> list = new List<Reservation>();

            var reservationsTemp = new List<Reservation>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44361/api/reservations/available");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.

                var parkUrl = "?start=" + start.Year + "/" + start.Month + "/" + start.Day + "T" + start.Hour + ":" + start.Minute + ":" + start.Second +
                    "&end=" + end.Year + "/" + end.Month + "/" + end.Day + "T" + end.Hour + ":" + end.Minute + ":" + end.Second;

                try
                {
                    //Sending request to find web api REST service resource Continents using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync(parkUrl);

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        reservationsTemp = JsonConvert.DeserializeObject<List<Reservation>>(EmpResponse);
                    }

                    reservations.AddRange(reservationsTemp);

                }

                catch (Exception)
                {
                }

            }


            return list;
        }

        public async Task<Reservation> PostReservation(DateTime start, DateTime end)
        {
            Reservation reservation = new Reservation();
            return reservation;
        }

        public async Task<Reservation> CancelReservation(DateTime start, DateTime end)
        {
            Reservation reservation = new Reservation();
            return reservation;
        }

        public async Task<string> GetToken(string username, string password)
        {
            using (var client = new HttpClient())
            {
                TokenRequestModel model = new TokenRequestModel() { Email = username, Password = password };

                client.BaseAddress = new Uri("http://localhost:44361/api/user/register");

                var postJob = client.PostAsJsonAsync<TokenRequestModel>("", model);

                postJob.Wait();

                

                var x = JsonConvert.DeserializeObject<AuthenticationModel>(postJob.Result.ToString());

                if (postResult.IsSuccessStatusCode)
                    return reservation;
            }
        }
    }
}
