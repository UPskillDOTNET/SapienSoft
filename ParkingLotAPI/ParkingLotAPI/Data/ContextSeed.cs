using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotAPI.Data;
using ParkingLotAPI.Models;

namespace ParkingLotAPI.Controllers
{
    public class ContextSeed
    {
        public static void SeedDb (ParkingLotAPIContext context)
        {
            context.Database.EnsureCreated();
            if(context.Slot.Any())
            {
                return;
            }

            var slots = new Slot[]
            {
                new Slot
                {
                    Place = 77, Row = "A", Floor = 0, Availability = "Available", Price = 2.75, Latitude = 12, Longitude = -3, IsEletricChargeAvailable = false
                }
            };

            foreach (Slot s in slots)
            {
                context.Slot.Add(s);
            }

            context.SaveChanges();
        }
    }
}
