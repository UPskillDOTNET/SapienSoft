using PublicParkAPI.Contexts;
using PublicParkAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublicParkAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace PublicParkAPI.Repositories.Slots
{
    public class SlotRepository : BaseRepository<Slot>, ISlotRepository
    {
        public SlotRepository(ApplicationDbContext context) : base (context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Slot>>> GetAllSlotsAsync()
        {
            return await _context.Slots.Include(s => s.Status).ToListAsync();
        }

        public async Task<ActionResult<Slot>> GetSlotById(int id)
        {
            return await _context.Slots.FindAsync(id);
        }
    }
}
