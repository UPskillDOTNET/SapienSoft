using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context { get; set; }

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<T>>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}
