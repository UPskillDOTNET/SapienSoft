using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Park2API.Entities;
using Park2API.Models;

namespace Park2API.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Slot> Slots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Discount> DailyDiscounts { get; set; }

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
                new Slot { Id = "A03", PricePerHour = 0, Status = "Reserved" },
                new Slot { Id = "A04", PricePerHour = 0, Status = "Reserved" },
                new Slot { Id = "A05", PricePerHour = 0, Status = "Reserved" },
                new Slot { Id = "B01", PricePerHour = 0, Status = "Reserved" },
                new Slot { Id = "B02", PricePerHour = 0, Status = "Reserved" },
                new Slot { Id = "B03", PricePerHour = 0, Status = "Reserved" },
                new Slot { Id = "B04", PricePerHour = 0, Status = "Reserved" },
                new Slot { Id = "B05", PricePerHour = 0, Status = "Reserved" });
        }
    }
}
