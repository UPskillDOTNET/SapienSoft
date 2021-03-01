using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SuperMammoth.Globals
{
    public static class Client
    {
        public static HttpClient WebApiClient = new HttpClient();

        static Client()
        {
            WebApiClient.BaseAddress = new Uri("https://localhost:44398/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
