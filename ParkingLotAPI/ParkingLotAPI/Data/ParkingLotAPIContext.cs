using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Models;

namespace ParkingLotAPI.Data
{
    public class ParkingLotAPIContext : DbContext
    {
        public ParkingLotAPIContext (DbContextOptions<ParkingLotAPIContext> options)
            : base(options)
        {
        }

        public DbSet<ParkingLotAPI.Models.Slot> Slot { get; set; }
    }
}
