using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Entities;
using PublicParkAPI.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Slot> Slots { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Available" },
                new Status { Id = 2, Name = "Reserved" });
            builder.Entity<Slot>().HasData(
                new Slot { Id = 1, Locator = "A01", Latitude = 30, Longitude = -8, PricePerHour = 1.0, StatusId = 1 },
                new Slot { Id = 2, Locator = "A02", Latitude = 30, Longitude = -8, PricePerHour = 1.0, StatusId = 1 },
                new Slot { Id = 3, Locator = "A03", Latitude = 30, Longitude = -8, PricePerHour = 1.0, StatusId = 1 });
        }
    }
}
