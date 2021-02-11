using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Models
{
    public class PaymentModel
    {
        public double Amount { get; set; }
        public string Source { get; set; }
        public string Currency { get; set; }
        public string Stripe_account { get; set; }
        public string Description { get; set; }
        public string Test { get; set; }
    }
}
