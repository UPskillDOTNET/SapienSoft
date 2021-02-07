using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParkAPI.Entities;

namespace ParkAPI.Contexts
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
                new Slot { Id = 1, Locator = "A01", Latitude = 41.17863016255019, Longitude = -8.608977694694504, PricePerHour = 1.0, StatusId = 1 }, // ISEP
                new Slot { Id = 2, Locator = "A02", Latitude = 41.17649696678654, Longitude = -8.605555050231363, PricePerHour = 1.0, StatusId = 1 }, // PORTIC
                new Slot { Id = 3, Locator = "A03", Latitude = 41.1785144293712, Longitude = -8.595926814809939, PricePerHour = 1.0, StatusId = 1 }, // FEUP
                new Slot { Id = 4, Locator = "A04", Latitude = 41.18163863102682, Longitude = -8.60108298949539, PricePerHour = 1.0, StatusId = 1 }, // HSJ
                new Slot { Id = 5, Locator = "A05", Latitude = 41.17538811531022, Longitude = -8.598801313339564, PricePerHour = 1.0, StatusId = 1 }); // Lisboa
        }
    }
}
