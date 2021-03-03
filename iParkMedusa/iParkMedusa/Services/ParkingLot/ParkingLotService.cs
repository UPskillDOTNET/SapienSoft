using iParkMedusa.Constants;
using iParkMedusa.Entities;
using iParkMedusa.Models;
using iParkMedusa.Repositories;
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
    public class ParkingLotService : IParkingLotService
    {
        private readonly ParkingLotSecrets _parkingLotSecrets;
        private readonly IParkRepository _repo;

        public ParkingLotService(IParkRepository repo, IOptions<ParkingLotSecrets> parkingLotSecrets)
        {
            _repo = repo;
            _parkingLotSecrets = parkingLotSecrets.Value ?? throw new ArgumentException(nameof(parkingLotSecrets));

        }

        public async Task<List<ReservationDTO>> GetAvailable(DateTime start, DateTime end)
        {
            var uriList = new List<string>();
            var availableList = new List<ReservationDTO>();
            var parkList = await _repo.FindAllAsync();
            
            foreach (var park in parkList)
            {
                uriList.Add(park.Uri);
            }
            
            foreach (var uri in uriList)
            {
                using (var client = new HttpClient())
                { 
                    client.BaseAddress = new Uri(uri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    TokenRequestModel trm = new TokenRequestModel() { Email = _parkingLotSecrets.Email, Password = _parkingLotSecrets.Password };

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
                    foreach (var item in list)
                    {
                        availableList.Add(item);
                    } 
                }
            }
            return availableList;
        }

        public async Task<ReservationDTO> PostReservation(DateTime start, DateTime end, string uri, int slotId)
        {
            using (var client = new HttpClient())
            {
                
                // Get Token
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                TokenRequestModel trm = new TokenRequestModel() { Email = _parkingLotSecrets.Email, Password = _parkingLotSecrets.Password };

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
                else
                {
                    return null;
                }

            }

        }
        public async Task<string> CancelReservation(Park park, int id)
        {

            using (var client = new HttpClient())
            {
                // Get Token
                client.BaseAddress = new Uri(park.Uri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                TokenRequestModel trm = new TokenRequestModel() { Email = _parkingLotSecrets.Email, Password = _parkingLotSecrets.Password };

                Task<HttpResponseMessage> response = client.PostAsJsonAsync("api/user/token", trm);
                var authenticationModel = await response.Result.Content.ReadFromJsonAsync<AuthenticationModel>();
                var token = authenticationModel.Token;

                // Insert Token
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                // Post Request
                Task<HttpResponseMessage> response2 = client.DeleteAsync("api/reservations/" + id);
                if (response2.Result.IsSuccessStatusCode)
                {

                    return "Deleted";
                }
                else
                {
                    return "Not Deleted";
                }
            }

        }
    }
}
