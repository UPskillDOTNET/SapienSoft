using Microsoft.EntityFrameworkCore;
using PaxAPI.Contexts;
using PaxAPI.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace PaxAPI.Repositories
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        public StatusRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Status> GetStatusByIdAsync(int id)
        {
            return await _context.Status.Where(s => s.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<int> DeleteStatusByIdAsync(int id)
        {
            var slot = await _context.Status.FindAsync(id);
            _context.Status.Remove(slot);
            return await _context.SaveChangesAsync();
        }
    }
}
