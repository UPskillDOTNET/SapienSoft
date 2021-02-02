using iParkCentralAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iParkCentralAPI.Entities;

namespace iParkCentralAPI.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {

            }
<<<<<<< HEAD
        public DbSet<iParkCentralAPI.Entities.Reservation> Reservation { get; set; }
        public DbSet<iParkCentralAPI.Entities.Park> Park { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<Park>().HasData(
                new Park { Id=1, Name = "Park 1", OwnerContact = "cmp@cm-porto.pt", IsChargingAvailable = true, BaseUrl = "http://localhost:49161/" },
                new Park { Id=2, Name = "Park 2", OwnerContact = "portic@ipp.pt", IsChargingAvailable = false, BaseUrl = "http://localhost:49162/", Latitude = 41.17652121446711, Longitude = -8.60566234613385, IsCovered = false }
                );
        }
=======
        public DbSet<Park> Park { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
>>>>>>> d61f763cc7bd6ed33281346b7c9f494ce95221a3
    }
}
