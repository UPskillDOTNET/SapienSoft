using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaxAPI.Entities;
using System.Collections.Generic;

namespace PaxAPI.Contexts
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
            /*builder.Entity<Park>().HasData(
           new Park { Id = 1, Latitude = 35.68231894113954, Longitude = 139.74984814724567, Address = "Chiyoda City, Tokyo 100-0001", SocialDistancingProgram = false });*/
            builder.Entity<Status>().HasData(
           new Status { Id = 1, Name = "Available" },
                new Status { Id = 2, Name = "Reserved" });
            builder.Entity<VehicleType>().HasData(
           new VehicleType { Id = 1, Name = "A", Description = "Motorcycles" },
                new VehicleType { Id = 2, Name = "B", Description = "Cars" },
                new VehicleType { Id = 3, Name = "C", Description = "Trucks" },
                new VehicleType { Id = 4, Name = "D", Description = "With Trailer" });
            builder.Entity<Slot>().HasData(
           new Slot { Id = 1, Locator = "A01", PricePerHour = 5, StatusId = 1, IsChargingAvailable = true, HasVallet = true, IsCovered = true, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = true, VehicleType = new List<VehicleType>(), PrioritySlot = true },
                new Slot { Id = 2, Locator = "A02", PricePerHour = 5, StatusId = 1, IsChargingAvailable = true, HasVallet = true, IsCovered = true, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = true, VehicleType = new List<VehicleType>(), PrioritySlot = true },
                new Slot { Id = 3, Locator = "A03", PricePerHour = 5, StatusId = 2, IsChargingAvailable = true, HasVallet = true, IsCovered = true, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = true, VehicleType = new List<VehicleType>(), PrioritySlot = false },
                new Slot { Id = 4, Locator = "A04", PricePerHour = 5, StatusId = 1, IsChargingAvailable = true, HasVallet = true, IsCovered = true, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = true, VehicleType = new List<VehicleType>(), PrioritySlot = false },
                new Slot { Id = 5, Locator = "A05", PricePerHour = 5, StatusId = 1, IsChargingAvailable = true, HasVallet = true, IsCovered = true, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = true, VehicleType = new List<VehicleType>(), PrioritySlot = false },
                new Slot { Id = 6, Locator = "A06", PricePerHour = 5, StatusId = 1, IsChargingAvailable = true, HasVallet = true, IsCovered = true, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = true, VehicleType = new List<VehicleType>(), PrioritySlot = false },
                new Slot { Id = 7, Locator = "B01", PricePerHour = 2, StatusId = 1, IsChargingAvailable = true, HasVallet = false, IsCovered = true, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = false, VehicleType = new List<VehicleType>(), PrioritySlot = false },
                new Slot { Id = 8, Locator = "B02", PricePerHour = 2, StatusId = 1, IsChargingAvailable = true, HasVallet = false, IsCovered = true, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = false, VehicleType = new List<VehicleType>(), PrioritySlot = false },
                new Slot { Id = 9, Locator = "B03", PricePerHour = 2, StatusId = 1, IsChargingAvailable = true, HasVallet = false, IsCovered = true, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = false, VehicleType = new List<VehicleType>(), PrioritySlot = false },
                new Slot { Id = 10, Locator = "B04", PricePerHour = 2, StatusId = 1, IsChargingAvailable = true, HasVallet = false, IsCovered = true, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = false, VehicleType = new List<VehicleType>(), PrioritySlot = false },
                new Slot { Id = 11, Locator = "B05", PricePerHour = 2, StatusId = 1, IsChargingAvailable = true, HasVallet = false, IsCovered = true, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = false, VehicleType = new List<VehicleType>(), PrioritySlot = false },
                new Slot { Id = 12, Locator = "B06", PricePerHour = 2, StatusId = 1, IsChargingAvailable = true, HasVallet = false, IsCovered = true, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = false, VehicleType = new List<VehicleType>(), PrioritySlot = false },
                new Slot { Id = 13, Locator = "C01", PricePerHour = 1.2, StatusId = 1, IsChargingAvailable = false, HasVallet = false, IsCovered = false, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = false, VehicleType = new List<VehicleType>(), PrioritySlot = false },
                new Slot { Id = 14, Locator = "C02", PricePerHour = 1.2, StatusId = 1, IsChargingAvailable = false, HasVallet = false, IsCovered = false, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = false, VehicleType = new List<VehicleType>(), PrioritySlot = false },
                new Slot { Id = 15, Locator = "C03", PricePerHour = 1.2, StatusId = 1, IsChargingAvailable = false, HasVallet = false, IsCovered = false, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = false, VehicleType = new List<VehicleType>(), PrioritySlot = false },
                new Slot { Id = 16, Locator = "C04", PricePerHour = 1.2, StatusId = 1, IsChargingAvailable = false, HasVallet = false, IsCovered = false, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = false, VehicleType = new List<VehicleType>(), PrioritySlot = false },
                new Slot { Id = 17, Locator = "C05", PricePerHour = 1.2, StatusId = 1, IsChargingAvailable = false, HasVallet = false, IsCovered = false, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = false, VehicleType = new List<VehicleType>(), PrioritySlot = false },
                new Slot { Id = 18, Locator = "C06", PricePerHour = 1.2, StatusId = 1, IsChargingAvailable = false, HasVallet = false, IsCovered = false, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = false, VehicleType = new List<VehicleType>(), PrioritySlot = false });
        }
    }
}
