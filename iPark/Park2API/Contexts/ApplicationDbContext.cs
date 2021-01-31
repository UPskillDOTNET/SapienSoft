using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Park2API.Entities;
using Park2API.Models;
using System;

namespace Park2API.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Slot> Slots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Discount> Discounts { get; set; }

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

            builder.Entity<Status>().HasData(
                new Status { Id = "Reserved", Description = "Slot reserved for internal use." },
                new Status { Id = "Available", Description = "Slot available for external booking." },
                new Status { Id = "Hotel", Description = "Slot reserved for Hotel use only." });

            builder.Entity<Slot>().HasData(
                new Slot { Id = "A01", PricePerHour = 0, Status = "Reserved" },
                new Slot { Id = "A02", PricePerHour = 0, Status = "Reserved" },
                new Slot { Id = "A03", PricePerHour = 2.15m, Status = "Available" },
                new Slot { Id = "A04", PricePerHour = 2.15m, Status = "Available" },
                new Slot { Id = "A05", PricePerHour = 2.15m, Status = "Available" },
                new Slot { Id = "B01", PricePerHour = 0.50m, Status = "Hotel" },
                new Slot { Id = "B02", PricePerHour = 0.50m, Status = "Reserved" },
                new Slot { Id = "B03", PricePerHour = 1.90m, Status = "Available" },
                new Slot { Id = "B04", PricePerHour = 1.90m, Status = "Available" },
                new Slot { Id = "B05", PricePerHour = 1.90m, Status = "Available" });

            builder.Entity<Discount>().HasData(
                new Discount { Id = 1, WeekDay = (DayOfWeek)1, Hour = 0, Rate = 1.0m },
                new Discount { Id = 2, WeekDay = (DayOfWeek)1, Hour = 1, Rate = 1.0m },
                new Discount { Id = 3, WeekDay = (DayOfWeek)1, Hour = 2, Rate = 1.0m },
                new Discount { Id = 4, WeekDay = (DayOfWeek)1, Hour = 3, Rate = 1.0m },
                new Discount { Id = 5, WeekDay = (DayOfWeek)1, Hour = 4, Rate = 1.0m },
                new Discount { Id = 6, WeekDay = (DayOfWeek)1, Hour = 5, Rate = 1.0m },
                new Discount { Id = 7, WeekDay = (DayOfWeek)1, Hour = 6, Rate = 1.0m },
                new Discount { Id = 8, WeekDay = (DayOfWeek)1, Hour = 7, Rate = 1.0m },
                new Discount { Id = 9, WeekDay = (DayOfWeek)1, Hour = 8, Rate = 1.0m },
                new Discount { Id = 10, WeekDay = (DayOfWeek)1, Hour = 9, Rate = 1.0m },
                new Discount { Id = 11, WeekDay = (DayOfWeek)1, Hour = 10, Rate = 1.0m },
                new Discount { Id = 12, WeekDay = (DayOfWeek)1, Hour = 11, Rate = 1.0m },
                new Discount { Id = 13, WeekDay = (DayOfWeek)1, Hour = 12, Rate = 1.0m },
                new Discount { Id = 14, WeekDay = (DayOfWeek)1, Hour = 13, Rate = 1.0m },
                new Discount { Id = 15, WeekDay = (DayOfWeek)1, Hour = 14, Rate = 1.0m },
                new Discount { Id = 16, WeekDay = (DayOfWeek)1, Hour = 15, Rate = 1.0m },
                new Discount { Id = 17, WeekDay = (DayOfWeek)1, Hour = 16, Rate = 1.0m },
                new Discount { Id = 18, WeekDay = (DayOfWeek)1, Hour = 17, Rate = 1.0m },
                new Discount { Id = 19, WeekDay = (DayOfWeek)1, Hour = 18, Rate = 1.0m },
                new Discount { Id = 20, WeekDay = (DayOfWeek)1, Hour = 19, Rate = 1.0m },
                new Discount { Id = 21, WeekDay = (DayOfWeek)1, Hour = 20, Rate = 1.0m },
                new Discount { Id = 22, WeekDay = (DayOfWeek)1, Hour = 21, Rate = 1.0m },
                new Discount { Id = 23, WeekDay = (DayOfWeek)1, Hour = 22, Rate = 1.0m },
                new Discount { Id = 24, WeekDay = (DayOfWeek)1, Hour = 23, Rate = 1.0m },
                new Discount { Id = 25, WeekDay = (DayOfWeek)2, Hour = 0, Rate = 1.0m },
                new Discount { Id = 26, WeekDay = (DayOfWeek)2, Hour = 1, Rate = 1.0m },
                new Discount { Id = 27, WeekDay = (DayOfWeek)2, Hour = 2, Rate = 1.0m },
                new Discount { Id = 28, WeekDay = (DayOfWeek)2, Hour = 3, Rate = 1.0m },
                new Discount { Id = 29, WeekDay = (DayOfWeek)2, Hour = 4, Rate = 1.0m },
                new Discount { Id = 30, WeekDay = (DayOfWeek)2, Hour = 5, Rate = 1.0m },
                new Discount { Id = 31, WeekDay = (DayOfWeek)2, Hour = 6, Rate = 1.0m },
                new Discount { Id = 32, WeekDay = (DayOfWeek)2, Hour = 7, Rate = 1.0m },
                new Discount { Id = 33, WeekDay = (DayOfWeek)2, Hour = 8, Rate = 1.0m },
                new Discount { Id = 34, WeekDay = (DayOfWeek)2, Hour = 9, Rate = 1.0m },
                new Discount { Id = 35, WeekDay = (DayOfWeek)2, Hour = 10, Rate = 1.0m },
                new Discount { Id = 36, WeekDay = (DayOfWeek)2, Hour = 11, Rate = 1.0m },
                new Discount { Id = 37, WeekDay = (DayOfWeek)2, Hour = 12, Rate = 1.0m },
                new Discount { Id = 38, WeekDay = (DayOfWeek)2, Hour = 13, Rate = 1.0m },
                new Discount { Id = 39, WeekDay = (DayOfWeek)2, Hour = 14, Rate = 1.0m },
                new Discount { Id = 40, WeekDay = (DayOfWeek)2, Hour = 15, Rate = 1.0m },
                new Discount { Id = 41, WeekDay = (DayOfWeek)2, Hour = 16, Rate = 1.0m },
                new Discount { Id = 42, WeekDay = (DayOfWeek)2, Hour = 17, Rate = 1.0m },
                new Discount { Id = 43, WeekDay = (DayOfWeek)2, Hour = 18, Rate = 1.0m },
                new Discount { Id = 44, WeekDay = (DayOfWeek)2, Hour = 19, Rate = 1.0m },
                new Discount { Id = 45, WeekDay = (DayOfWeek)2, Hour = 20, Rate = 1.0m },
                new Discount { Id = 46, WeekDay = (DayOfWeek)2, Hour = 21, Rate = 1.0m },
                new Discount { Id = 47, WeekDay = (DayOfWeek)2, Hour = 22, Rate = 1.0m },
                new Discount { Id = 48, WeekDay = (DayOfWeek)2, Hour = 23, Rate = 1.0m },
                new Discount { Id = 49, WeekDay = (DayOfWeek)3, Hour = 0, Rate = 1.0m },
                new Discount { Id = 50, WeekDay = (DayOfWeek)3, Hour = 1, Rate = 1.0m },
                new Discount { Id = 51, WeekDay = (DayOfWeek)3, Hour = 2, Rate = 1.0m },
                new Discount { Id = 52, WeekDay = (DayOfWeek)3, Hour = 3, Rate = 1.0m },
                new Discount { Id = 53, WeekDay = (DayOfWeek)3, Hour = 4, Rate = 1.0m },
                new Discount { Id = 54, WeekDay = (DayOfWeek)3, Hour = 5, Rate = 1.0m },
                new Discount { Id = 55, WeekDay = (DayOfWeek)3, Hour = 6, Rate = 1.0m },
                new Discount { Id = 56, WeekDay = (DayOfWeek)3, Hour = 7, Rate = 1.0m },
                new Discount { Id = 57, WeekDay = (DayOfWeek)3, Hour = 8, Rate = 1.0m },
                new Discount { Id = 58, WeekDay = (DayOfWeek)3, Hour = 9, Rate = 1.0m },
                new Discount { Id = 59, WeekDay = (DayOfWeek)3, Hour = 10, Rate = 1.0m },
                new Discount { Id = 60, WeekDay = (DayOfWeek)3, Hour = 11, Rate = 1.0m },
                new Discount { Id = 61, WeekDay = (DayOfWeek)3, Hour = 12, Rate = 1.0m },
                new Discount { Id = 62, WeekDay = (DayOfWeek)3, Hour = 13, Rate = 1.0m },
                new Discount { Id = 63, WeekDay = (DayOfWeek)3, Hour = 14, Rate = 1.0m },
                new Discount { Id = 64, WeekDay = (DayOfWeek)3, Hour = 15, Rate = 1.0m },
                new Discount { Id = 65, WeekDay = (DayOfWeek)3, Hour = 16, Rate = 1.0m },
                new Discount { Id = 66, WeekDay = (DayOfWeek)3, Hour = 17, Rate = 1.0m },
                new Discount { Id = 67, WeekDay = (DayOfWeek)3, Hour = 18, Rate = 1.0m },
                new Discount { Id = 68, WeekDay = (DayOfWeek)3, Hour = 19, Rate = 1.0m },
                new Discount { Id = 69, WeekDay = (DayOfWeek)3, Hour = 20, Rate = 1.0m },
                new Discount { Id = 70, WeekDay = (DayOfWeek)3, Hour = 21, Rate = 1.0m },
                new Discount { Id = 71, WeekDay = (DayOfWeek)3, Hour = 22, Rate = 1.0m },
                new Discount { Id = 72, WeekDay = (DayOfWeek)3, Hour = 23, Rate = 1.0m },
                new Discount { Id = 73, WeekDay = (DayOfWeek)4, Hour = 0, Rate = 1.0m },
                new Discount { Id = 74, WeekDay = (DayOfWeek)4, Hour = 1, Rate = 1.0m },
                new Discount { Id = 75, WeekDay = (DayOfWeek)4, Hour = 2, Rate = 1.0m },
                new Discount { Id = 76, WeekDay = (DayOfWeek)4, Hour = 3, Rate = 1.0m },
                new Discount { Id = 77, WeekDay = (DayOfWeek)4, Hour = 4, Rate = 1.0m },
                new Discount { Id = 78, WeekDay = (DayOfWeek)4, Hour = 5, Rate = 1.0m },
                new Discount { Id = 79, WeekDay = (DayOfWeek)4, Hour = 6, Rate = 1.0m },
                new Discount { Id = 80, WeekDay = (DayOfWeek)4, Hour = 7, Rate = 1.0m },
                new Discount { Id = 81, WeekDay = (DayOfWeek)4, Hour = 8, Rate = 1.0m },
                new Discount { Id = 82, WeekDay = (DayOfWeek)4, Hour = 9, Rate = 1.0m },
                new Discount { Id = 83, WeekDay = (DayOfWeek)4, Hour = 10, Rate = 1.0m },
                new Discount { Id = 84, WeekDay = (DayOfWeek)4, Hour = 11, Rate = 1.0m },
                new Discount { Id = 85, WeekDay = (DayOfWeek)4, Hour = 12, Rate = 1.0m },
                new Discount { Id = 86, WeekDay = (DayOfWeek)4, Hour = 13, Rate = 1.0m },
                new Discount { Id = 87, WeekDay = (DayOfWeek)4, Hour = 14, Rate = 1.0m },
                new Discount { Id = 88, WeekDay = (DayOfWeek)4, Hour = 15, Rate = 1.0m },
                new Discount { Id = 89, WeekDay = (DayOfWeek)4, Hour = 16, Rate = 1.0m },
                new Discount { Id = 90, WeekDay = (DayOfWeek)4, Hour = 17, Rate = 1.0m },
                new Discount { Id = 91, WeekDay = (DayOfWeek)4, Hour = 18, Rate = 1.0m },
                new Discount { Id = 92, WeekDay = (DayOfWeek)4, Hour = 19, Rate = 1.0m },
                new Discount { Id = 93, WeekDay = (DayOfWeek)4, Hour = 20, Rate = 1.0m },
                new Discount { Id = 94, WeekDay = (DayOfWeek)4, Hour = 21, Rate = 1.0m },
                new Discount { Id = 95, WeekDay = (DayOfWeek)4, Hour = 22, Rate = 1.0m },
                new Discount { Id = 96, WeekDay = (DayOfWeek)4, Hour = 23, Rate = 1.0m },
                new Discount { Id = 97, WeekDay = (DayOfWeek)5, Hour = 0, Rate = 1.0m },
                new Discount { Id = 98, WeekDay = (DayOfWeek)5, Hour = 1, Rate = 1.0m },
                new Discount { Id = 99, WeekDay = (DayOfWeek)5, Hour = 2, Rate = 1.0m },
                new Discount { Id = 100, WeekDay = (DayOfWeek)5, Hour = 3, Rate = 1.0m },
                new Discount { Id = 101, WeekDay = (DayOfWeek)5, Hour = 4, Rate = 1.0m },
                new Discount { Id = 102, WeekDay = (DayOfWeek)5, Hour = 5, Rate = 1.0m },
                new Discount { Id = 103, WeekDay = (DayOfWeek)5, Hour = 6, Rate = 1.0m },
                new Discount { Id = 104, WeekDay = (DayOfWeek)5, Hour = 7, Rate = 1.0m },
                new Discount { Id = 105, WeekDay = (DayOfWeek)5, Hour = 8, Rate = 1.0m },
                new Discount { Id = 106, WeekDay = (DayOfWeek)5, Hour = 9, Rate = 1.0m },
                new Discount { Id = 107, WeekDay = (DayOfWeek)5, Hour = 10, Rate = 1.0m },
                new Discount { Id = 108, WeekDay = (DayOfWeek)5, Hour = 11, Rate = 1.0m },
                new Discount { Id = 109, WeekDay = (DayOfWeek)5, Hour = 12, Rate = 1.0m },
                new Discount { Id = 110, WeekDay = (DayOfWeek)5, Hour = 13, Rate = 1.0m },
                new Discount { Id = 111, WeekDay = (DayOfWeek)5, Hour = 14, Rate = 1.0m },
                new Discount { Id = 112, WeekDay = (DayOfWeek)5, Hour = 15, Rate = 1.0m },
                new Discount { Id = 113, WeekDay = (DayOfWeek)5, Hour = 16, Rate = 1.0m },
                new Discount { Id = 114, WeekDay = (DayOfWeek)5, Hour = 17, Rate = 1.0m },
                new Discount { Id = 115, WeekDay = (DayOfWeek)5, Hour = 18, Rate = 1.0m },
                new Discount { Id = 116, WeekDay = (DayOfWeek)5, Hour = 19, Rate = 1.0m },
                new Discount { Id = 117, WeekDay = (DayOfWeek)5, Hour = 20, Rate = 1.0m },
                new Discount { Id = 118, WeekDay = (DayOfWeek)5, Hour = 21, Rate = 1.0m },
                new Discount { Id = 119, WeekDay = (DayOfWeek)5, Hour = 22, Rate = 1.0m },
                new Discount { Id = 120, WeekDay = (DayOfWeek)5, Hour = 23, Rate = 1.0m },
                new Discount { Id = 121, WeekDay = (DayOfWeek)6, Hour = 0, Rate = 1.0m },
                new Discount { Id = 122, WeekDay = (DayOfWeek)6, Hour = 1, Rate = 1.0m },
                new Discount { Id = 123, WeekDay = (DayOfWeek)6, Hour = 2, Rate = 1.0m },
                new Discount { Id = 124, WeekDay = (DayOfWeek)6, Hour = 3, Rate = 1.0m },
                new Discount { Id = 125, WeekDay = (DayOfWeek)6, Hour = 4, Rate = 1.0m },
                new Discount { Id = 126, WeekDay = (DayOfWeek)6, Hour = 5, Rate = 1.0m },
                new Discount { Id = 127, WeekDay = (DayOfWeek)6, Hour = 6, Rate = 1.0m },
                new Discount { Id = 128, WeekDay = (DayOfWeek)6, Hour = 7, Rate = 1.0m },
                new Discount { Id = 129, WeekDay = (DayOfWeek)6, Hour = 8, Rate = 1.0m },
                new Discount { Id = 130, WeekDay = (DayOfWeek)6, Hour = 9, Rate = 1.0m },
                new Discount { Id = 131, WeekDay = (DayOfWeek)6, Hour = 10, Rate = 1.0m },
                new Discount { Id = 132, WeekDay = (DayOfWeek)6, Hour = 11, Rate = 1.0m },
                new Discount { Id = 133, WeekDay = (DayOfWeek)6, Hour = 12, Rate = 1.0m },
                new Discount { Id = 134, WeekDay = (DayOfWeek)6, Hour = 13, Rate = 1.0m },
                new Discount { Id = 135, WeekDay = (DayOfWeek)6, Hour = 14, Rate = 1.0m },
                new Discount { Id = 136, WeekDay = (DayOfWeek)6, Hour = 15, Rate = 1.0m },
                new Discount { Id = 137, WeekDay = (DayOfWeek)6, Hour = 16, Rate = 1.0m },
                new Discount { Id = 138, WeekDay = (DayOfWeek)6, Hour = 17, Rate = 1.0m },
                new Discount { Id = 139, WeekDay = (DayOfWeek)6, Hour = 18, Rate = 1.0m },
                new Discount { Id = 140, WeekDay = (DayOfWeek)6, Hour = 19, Rate = 1.0m },
                new Discount { Id = 141, WeekDay = (DayOfWeek)6, Hour = 20, Rate = 1.0m },
                new Discount { Id = 142, WeekDay = (DayOfWeek)6, Hour = 21, Rate = 1.0m },
                new Discount { Id = 143, WeekDay = (DayOfWeek)6, Hour = 22, Rate = 1.0m },
                new Discount { Id = 144, WeekDay = (DayOfWeek)6, Hour = 23, Rate = 1.0m },
                new Discount { Id = 145, WeekDay = 0, Hour = 0, Rate = 1.0m },
                new Discount { Id = 146, WeekDay = 0, Hour = 1, Rate = 1.0m },
                new Discount { Id = 147, WeekDay = 0, Hour = 2, Rate = 1.0m },
                new Discount { Id = 148, WeekDay = 0, Hour = 3, Rate = 1.0m },
                new Discount { Id = 149, WeekDay = 0, Hour = 4, Rate = 1.0m },
                new Discount { Id = 150, WeekDay = 0, Hour = 5, Rate = 1.0m },
                new Discount { Id = 151, WeekDay = 0, Hour = 6, Rate = 1.0m },
                new Discount { Id = 152, WeekDay = 0, Hour = 7, Rate = 1.0m },
                new Discount { Id = 153, WeekDay = 0, Hour = 8, Rate = 1.0m },
                new Discount { Id = 154, WeekDay = 0, Hour = 9, Rate = 1.0m },
                new Discount { Id = 155, WeekDay = 0, Hour = 10, Rate = 1.0m },
                new Discount { Id = 156, WeekDay = 0, Hour = 11, Rate = 1.0m },
                new Discount { Id = 157, WeekDay = 0, Hour = 12, Rate = 1.0m },
                new Discount { Id = 158, WeekDay = 0, Hour = 13, Rate = 1.0m },
                new Discount { Id = 159, WeekDay = 0, Hour = 14, Rate = 1.0m },
                new Discount { Id = 160, WeekDay = 0, Hour = 15, Rate = 1.0m },
                new Discount { Id = 161, WeekDay = 0, Hour = 16, Rate = 1.0m },
                new Discount { Id = 162, WeekDay = 0, Hour = 17, Rate = 1.0m },
                new Discount { Id = 163, WeekDay = 0, Hour = 18, Rate = 1.0m },
                new Discount { Id = 164, WeekDay = 0, Hour = 19, Rate = 1.0m },
                new Discount { Id = 165, WeekDay = 0, Hour = 20, Rate = 1.0m },
                new Discount { Id = 166, WeekDay = 0, Hour = 21, Rate = 1.0m },
                new Discount { Id = 167, WeekDay = 0, Hour = 22, Rate = 1.0m },
                new Discount { Id = 168, WeekDay = 0, Hour = 23, Rate = 1.0m });
        }
    }
}
