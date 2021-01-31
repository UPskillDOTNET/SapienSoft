using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Park2API.Entities
{
    public class Discount
    {
        public int Id { get; set; }

        public DayOfWeek WeekDay { get; set; }

        public int Hour { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        public decimal Rate { get; set; }
    }
}
