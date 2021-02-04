using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Entities
{
    public class Discount
    {
        public int Id { get; set; }

        public DayOfWeek WeekDay { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        public decimal Rate { get; set; }
    }
}
