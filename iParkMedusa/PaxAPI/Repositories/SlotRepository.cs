using Microsoft.EntityFrameworkCore;
using PaxAPI.Contexts;
using PaxAPI.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PaxAPI.Repositories
{
    public class SlotRepository : BaseRepository<Slot>, ISlotRepository
    {
        public SlotRepository(ApplicationDbContext context) : base (context)
        {
        }

        public async Task<IEnumerable<Slot>> GetAllSlotsAsync()
        {
            //return await Task.FromResult<List<Slot>>(_context.Slots.Include(s => s.Status).ToList());
            return await _context.Slots.Include(s => s.Status).ToListAsync();
        }

        public async Task<Slot> GetSlotByIdAsync(int id)
        {
            return await _context.Slots.Where(s => s.Id.Equals(id)).Include(s => s.Status).FirstOrDefaultAsync();
        }

        public async Task<List<Slot>> GetSlotsByStatus(string status)
        {
            return await _context.Slots.Where(s => s.Status.Name == status).ToListAsync();
        }

        public async Task<int> DeleteSlotByIdAsync(int id)
        {
            var slot = await _context.Slots.FindAsync(id);
            _context.Slots.Remove(slot);
            return await _context.SaveChangesAsync();
        }
    }
}
