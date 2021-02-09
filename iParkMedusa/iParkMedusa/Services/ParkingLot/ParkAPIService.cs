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

        public async Task<List<ReservationDTO>> GetAvailable(DateTime start, DateTime end)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                TokenRequestModel trm = new TokenRequestModel() { Email = username, Password = password };

                Task<HttpResponseMessage> response = client.PostAsJsonAsync("api/user/token", trm);
                var authenticationModel = await response.Result.Content.ReadFromJsonAsync<AuthenticationModel>();
                var token = authenticationModel.Token;

                var url = "api/reservations/available?start=" +
                    start.ToString("yyyy-MM-dd") + "T" + start.ToString("HH:mm:ss") + "&end=" +
                    end.ToString("yyyy-MM-dd") + "T" + end.ToString("HH:mm:ss");

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                response = client.GetAsync(url);
                var content = await response.Result.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<ReservationDTO>>(content);

                return list;
            }
        }
    }
}
