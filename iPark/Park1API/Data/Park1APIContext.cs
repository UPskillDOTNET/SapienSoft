using Microsoft.EntityFrameworkCore;
using Park1API.Models;

namespace Park1API.Data
{
    public class Park1APIContext : DbContext
    {
        public Park1APIContext(DbContextOptions<Park1APIContext> options) : base(options)
        {
        }

        public DbSet<Slot> Slots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
