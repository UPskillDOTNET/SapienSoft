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
    public class SlotRepository : ISlotRepository
    {
        protected ApplicationDbContext _context { get; set; }
        public SlotRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Slot>> GetAllSlotsAsync()
        {
            //return await Task.FromResult<List<Slot>>(_context.Slots.Include(s => s.Status).ToList());
            return await _context.Slots.Include(s => s.Status).ToListAsync();
        }

        public async Task<Slot> GetSlotByIdAsync(int id)
        {
            return await Task.FromResult<Slot>(_context.Slots.Where(s => s.Id.Equals(id)).Include(s => s.Status).SingleOrDefault());
        }

        public async Task<int> DeleteSlotByIdAsync(int id)
        {
            var slot = await _context.Slots.FindAsync(id);
            _context.Slots.Remove(slot);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateSlotAsync(Slot slot)
        {
            _context.Entry(slot).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
