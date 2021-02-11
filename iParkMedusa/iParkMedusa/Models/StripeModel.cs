using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Models
{
    public class StripeModel
    {
        public string CardNumber { get; set; }
        public int Cvc { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string Test { get; set; }
    }
}
