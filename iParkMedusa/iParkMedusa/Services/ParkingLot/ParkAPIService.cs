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

        public async Task<string> GetToken(string email, string password)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44365/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    TokenRequestModel trm = new TokenRequestModel() { Email = email, Password = password };

                    Task<HttpResponseMessage> response = client.PostAsJsonAsync("api/user/token", trm);
                    var tokenResult = await response.Result.Content.ReadFromJsonAsync<AuthenticationModel>();
                    var token = tokenResult.Token;

                    return token;
                }
            }
            catch (Exception ex)
            {
            }
            return "";
        }
    }
}
