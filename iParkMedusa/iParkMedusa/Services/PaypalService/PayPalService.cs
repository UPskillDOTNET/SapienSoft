using iParkMedusa.Models.PayPalModels;
using iParkMedusa.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace iParkMedusa.Services.PaypalService
{
    public class PayPalService
    {
        private readonly PayPalCredentials _payPalCredentials;

        public PayPalService(IOptions<PayPalCredentials> payPalCredentials)
        {
            _payPalCredentials = payPalCredentials.Value ?? throw new ArgumentException(nameof(payPalCredentials));
        }

        public async Task<string> GetPayPalToken()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Accept-Language", "en_US");

                var byteArray = Encoding.UTF8.GetBytes(_payPalCredentials.ClientID + ":" + _payPalCredentials.Secret);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                

                var requestParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                };
                var content = new FormUrlEncodedContent(requestParams);

                Task<HttpResponseMessage> response = client.PostAsync("v1/oauth2/token",content);
                var result = await response.Result.Content.ReadAsStringAsync();


                PayPalTokenModel tokenModel = JsonConvert.DeserializeObject<PayPalTokenModel>(result);

                return tokenModel.access_token;
            }
        }
    }
}
