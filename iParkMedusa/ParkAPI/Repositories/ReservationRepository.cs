using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Contexts;
using PublicParkAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations.Include(s => s.Slot).Include(u => u.ApplicationUser).ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            return await Task.FromResult(_context.Reservations.Where(s => s.Id.Equals(id)).SingleOrDefault());
        }

        public async Task<List<Reservation>> GetReservationsBySlotId(int slotId)
        {
            return await _context.Reservations.Where(s => s.SlotId == slotId).ToListAsync();
        }

        public async Task<int> DeleteReservationByIdAsync(int id)
        {
            var slot = await _context.Slots.FindAsync(id);
            _context.Slots.Remove(slot);
            return await _context.SaveChangesAsync();
        }
    }
}
