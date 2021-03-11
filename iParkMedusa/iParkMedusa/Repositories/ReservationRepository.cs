using iParkMedusa.Contexts;
using iParkMedusa.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            //return await Task.FromResult<List<Slot>>(_context.Slots.Include(s => s.Status).ToList());
            return await _context.Reservations.Include(u => u.ApplicationUser).Include(p => p.Park).ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            return await _context.Reservations.Where(s => s.Id == id).Include(u => u.ApplicationUser).Include(p => p.Park).FirstOrDefaultAsync();
        }

        public async Task<int> DeleteReservationByIdAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            return await _context.SaveChangesAsync();
        }
        public async Task<List<Reservation>> GetRentReservationsAsync(DateTime start, DateTime end)
        {
            return await _context.Reservations.Where(r => r.AvailableToRent == true).Where(r => r.Start <= start).Where(r=> r.End >= end).Include(p => p.Park).ToListAsync();
            
        }

        public async Task<List<Reservation>> GetReservationsByUserIdAsync(string userId)
        {
            return await _context.Reservations.Where(s => s.UserId == userId).Include(u => u.Park).ToListAsync();
        }
    }
}
