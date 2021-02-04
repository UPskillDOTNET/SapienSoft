using PublicParkAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublicParkAPI.Entities;
using Microsoft.AspNetCore.Identity;

namespace PublicParkAPI.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Slot> Slots { get; set; }
        public DbSet<SlotStatus> Status { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
            
            builder.Entity<SlotStatus>().HasData(
                new SlotStatus { Id = 1, Name = "Available", Description = "Slot reserved for internal use." },
                new SlotStatus { Id = 2, Name = "Reserved", Description = "Slot available for external booking." },
                new SlotStatus { Id = 3, Name = "Hotel", Description = "Slot reserved for Hotel use only." });
            
            builder.Entity<Slot>().HasData(
                new Slot { Id = 1, Locator = "A01", Latitude = 30, Longitude = -8, ECharging = false, PricePerHour = 1.0m, StatusId = 1 },
                new Slot { Id = 2, Locator = "A02", Latitude = 30, Longitude = -8, ECharging = false, PricePerHour = 1.0m, StatusId = 2 },
                new Slot { Id = 3, Locator = "A03", Latitude = 30, Longitude = -8, ECharging = false, PricePerHour = 1.0m, StatusId = 2 },
                new Slot { Id = 4, Locator = "A04", Latitude = 30, Longitude = -8, ECharging = false, PricePerHour = 1.0m, StatusId = 3 },
                new Slot { Id = 5, Locator = "A05", Latitude = 30, Longitude = -8, ECharging = true, PricePerHour = 1.0m, StatusId = 1 });

            builder.Entity<Discount>().HasData(
                new Discount { Id = 1, WeekDay = (DayOfWeek)0, Rate = 1.0m },
                new Discount { Id = 2, WeekDay = (DayOfWeek)1, Rate = 1.0m },
                new Discount { Id = 3, WeekDay = (DayOfWeek)2, Rate = 1.0m },
                new Discount { Id = 4, WeekDay = (DayOfWeek)3, Rate = 1.0m },
                new Discount { Id = 5, WeekDay = (DayOfWeek)4, Rate = 1.0m },
                new Discount { Id = 6, WeekDay = (DayOfWeek)5, Rate = 1.0m },
                new Discount { Id = 7, WeekDay = (DayOfWeek)6, Rate = 1.0m });
        }
    }
}
