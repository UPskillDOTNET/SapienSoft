using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Models
{
    public class RefMultibancoModel
    {
        public int Entidade { get; } = 10912;
        public int Referencia { get; set; }
        public double Valor { get; set; }
    }
}
