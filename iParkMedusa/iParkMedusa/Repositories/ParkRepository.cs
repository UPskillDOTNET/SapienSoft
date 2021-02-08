using iParkMedusa.Contexts;
using iParkMedusa.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Repositories
{
    public class ParkRepository : BaseRepository<Park>, IParkRepository
    {
        public ParkRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Park>> GetAllSlotsAsync()
        {
            //return await Task.FromResult<List<Slot>>(_context.Slots.Include(s => s.Status).ToList());
            return await _context.Parks.ToListAsync();
        }

        public async Task<Park> GetParkByIdAsync(int id)
        {
            return await _context.Parks.Where(s => s.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<int> DeleteParkByIdAsync(int id)
        {
            var slot = await _context.Parks.FindAsync(id);
            _context.Parks.Remove(slot);
            return await _context.SaveChangesAsync();
        }
    }
}
