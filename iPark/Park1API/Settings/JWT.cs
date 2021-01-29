using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Park1API.Settings
{
    /// <summary>
    /// This class will be used to read data from JWT Section of appsettings.json using the IOptions feature of ASP.NET Core
    /// </summary>
    public class JWT
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }
    }
}
