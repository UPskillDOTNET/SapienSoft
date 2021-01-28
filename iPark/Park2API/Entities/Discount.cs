using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Park2API.Entities
{

    public class Discount
    {
        [Key]
        public string TimeDivision { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        public decimal Rate { get; set; }

    }
}
