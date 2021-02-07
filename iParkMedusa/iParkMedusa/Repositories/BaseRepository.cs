using Microsoft.EntityFrameworkCore;
using iParkMedusa.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iParkMedusa.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context { get; set; }
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> FindAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<int> AddEntityAsync(T entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateEntityAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
