using Microsoft.EntityFrameworkCore;
using Park1API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Park1API.Data
{
    public class SlotDbContext : DbContext
    {
        public SlotDbContext(DbContextOptions<SlotDbContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slot>().HasMany(r => r.Reservations).WithOne(s => s.Slot).IsRequired();
            modelBuilder.Entity<User>().HasMany(r => r.Reservations).WithOne(u => u.User).IsRequired();
        }
    }
}
