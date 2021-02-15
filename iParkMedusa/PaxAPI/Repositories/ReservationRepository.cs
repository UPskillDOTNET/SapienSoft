using Microsoft.EntityFrameworkCore;
using PaxAPI.Contexts;
using PaxAPI.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaxAPI.Repositories
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
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            return await _context.SaveChangesAsync();
        }
    }
}
