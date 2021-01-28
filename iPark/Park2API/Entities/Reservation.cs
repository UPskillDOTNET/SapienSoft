using Park2API.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Park2API.Entities;
using System.Collections.Generic;

namespace Park2API.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Client { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime DateCreated { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        public decimal Value { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public int SlotId { get; set; }

        [ForeignKey("SlotId")]
        public Slot Slot { get; set; }

        /*private decimal GetValue(DateTime checkin, DateTime checkout, Slot slot, List<DailyDiscount> discounts)
        {
            double horas = (checkout - checkin).TotalHours;
            DateTime diaAux = checkin;
            decimal value = 0;
            for(int i=0; i<horas; i++)
            {
                var dia = diaAux.DayOfWeek; //  .....
                
                value += slot.PricePerHour * FUCKING_DISCOUNT;
            }
            
            foreach ()
            List<DateTime> estadia = new List<DateTime>();
            DateTime tmp = checkin;

            do
            {
                estadia.Add(tmp);
                tmp = tmp.AddDays(1);
            } while (tmp <= checkout);

            foreach (DateTime item in estadia)
            {
                item.DayOfWeek ;
            }
                var horas = (checkout - checkin).Hours;
            var value = horas * slot.PricePerHour;

            return value;
        }*/
    }
}
