using iPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPark.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Parks.Any())
            {
                return;   // DB has been seeded
            }

            var parks = new Park[]
            {
                new Park{ParkUrl="https://localhost:45678/api/", Name="Park 1", GeoLatitude= 41.17634467779482, GeoLongitude= -8.60560968231418},
                new Park{ParkUrl="https://localhost:45678/api/", Name="Park 2", GeoLatitude= 41.17862604315608, GeoLongitude= -8.608989183478442}
            };
            foreach (Park p in parks)
            {
                context.Parks.Add(p);
            }
            context.SaveChanges();


            
        }
    }
}
