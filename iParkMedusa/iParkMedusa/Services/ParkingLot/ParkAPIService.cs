using iParkMedusa.Entities;
using iParkMedusa.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace iParkMedusa.Services.ParkingLot
{
    public class ParkAPIService : IParkingLotService
    {
        private readonly string username = "sapiensoft@sapiensoft.com";
        private readonly string password = "SapienSoft123!";

        public async Task<List<ReservationDTO>> GetAvailableSlots(DateTime start, DateTime end)
        {
            TokenRequestModel tokenRequestModel = new TokenRequestModel()
            {
                Email = username,
                Password = password
            };

            using (var clientToken = new HttpClient())
            {
                clientToken.BaseAddress = new Uri("http://localhost:44365/");
                clientToken.DefaultRequestHeaders.Clear();
                clientToken.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await clientToken.PostAsJsonAsync("api/user/token", tokenRequestModel);
            }

            List<ReservationDTO> reservations = new List<ReservationDTO>();
                        
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44365/api/reservations/available");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
                        reservations = JsonConvert.DeserializeObject<List<ReservationDTO>>(EmpResponse);
                    }
                }

                catch (Exception)
                {
                }
            }

            return reservations;
        }
    }
}
