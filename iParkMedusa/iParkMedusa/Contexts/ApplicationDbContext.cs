using iParkMedusa.Entities;
using iParkMedusa.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<TransactionType>().HasData(
                new TransactionType { Id = 1, Name = "Debit - Reservation" },
                new TransactionType { Id = 2, Name = "Credit - Added Funds" },
                new TransactionType { Id = 3, Name = "Credit - Cancelation" });
            builder.Entity<Park>().HasData(
                new Park { Id = 1, Name = "Park1" },
                new Park { Id = 2, Name = "Park2" });
        }
    }
}
