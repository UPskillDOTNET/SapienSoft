using iParkMedusa.Entities;
using iParkMedusa.Models;
using iParkMedusa.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        private readonly ParkAPISecrets _parkAPISecrets;

        public ParkAPIService(IOptions<ParkAPISecrets> parkAPISecrets)
        {
            _parkAPISecrets = parkAPISecrets.Value ?? throw new ArgumentException(nameof(parkAPISecrets));
        }

        public async Task<List<ReservationDTO>> GetAvailable(DateTime start, DateTime end)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                TokenRequestModel trm = new TokenRequestModel() { Email = _parkAPISecrets.Email, Password = _parkAPISecrets.Password };

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

        public async Task<ReservationDTO> PostReservation(DateTime start, DateTime end, int slotId)
        {
            using (var client = new HttpClient())
            {
                // Get Token
                client.BaseAddress = new Uri("https://localhost:44365/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                TokenRequestModel trm = new TokenRequestModel() { Email = _parkAPISecrets.Email, Password = _parkAPISecrets.Password };

                Task<HttpResponseMessage> response = client.PostAsJsonAsync("api/user/token", trm);
                var authenticationModel = await response.Result.Content.ReadFromJsonAsync<AuthenticationModel>();
                var token = authenticationModel.Token;

                // Insert Token
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                // Create Json Body
                ReservationDTO reservationDTO = new ReservationDTO() { Start = start, End = end, SlotId = slotId };

                // Post Request
                Task<HttpResponseMessage> response2 = client.PostAsJsonAsync("api/reservations/booking", reservationDTO);
                if (response2.Result.IsSuccessStatusCode)
                {
                    var reservation = await response2.Result.Content.ReadFromJsonAsync<ReservationDTO>();
                    return reservation;
                }
                else if (response2.Result.StatusCode.Equals(400))
                {
                }

                return new ReservationDTO();
            }
        }
    }
}
