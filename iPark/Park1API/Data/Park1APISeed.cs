using Park1API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Park1API.Data
{
    public class Park1APISeed
    {
        public static void Initialize(Park1APIContext context)
        {
            context.Database.EnsureCreated();

            // Look for any continents.
            if (context.Slots.Any())
            {
                return;   // DB has been seeded
            }

            var slots = new Slot[]
            {
            new Slot{LocationCode = "L01", Status = Status.Reserved},
            new Slot{LocationCode = "L02", Status = Status.Reserved},
            new Slot{LocationCode = "L03", Status = Status.Booked},
            new Slot{LocationCode = "L04", Status = Status.Booked},
            new Slot{LocationCode = "L05", Status = Status.Booked},
            new Slot{LocationCode = "L06", Status = Status.Booked},
            new Slot{LocationCode = "L07", Status = Status.Available},
            new Slot{LocationCode = "L08", Status = Status.Available},
            new Slot{LocationCode = "L09", Status = Status.Available},
            new Slot{LocationCode = "L10", Status = Status.Available}
            };
            foreach (Slot s in slots)
            {
                context.Slots.Add(s);
            }
            context.SaveChanges();

            var reservations = new Reservation[]
            {
            new Reservation{Start = new DateTime(2021, 01, 23, 09, 0, 0), End = new DateTime(2021, 01, 23, 12, 0, 0), SlotId = 2, UserId = 0},
            new Reservation{Start = new DateTime(2021, 01, 23, 18, 0, 0), End = new DateTime(2021, 01, 24, 09, 0, 0), SlotId = 3, UserId = 0},
            new Reservation{Start = new DateTime(2021, 01, 25, 09, 0, 0), End = new DateTime(2020, 01, 12, 18, 0, 0), SlotId = 4, UserId = 0},
            new Reservation{Start = new DateTime(2021, 01, 27, 00, 0, 0), End = new DateTime(2020, 02, 27, 00, 0, 0), SlotId = 5, UserId = 0},
            };
            foreach (Reservation r in reservations)
            {
                context.Reservations.Add(r);
            }
            context.SaveChanges();
        }
    }
}
