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
        public DbSet<DailyDiscount> DailyDiscounts { get; set; }
    }
}
