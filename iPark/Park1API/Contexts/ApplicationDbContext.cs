using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Park1API.Entities;
using Park1API.Models;

namespace Park1API.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Slot> Slots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Discount> DailyDiscounts { get; set; }
        public DbSet<Park1API.Entities.Status> Status { get; set; }
    }
}
