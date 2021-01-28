using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Park2API.Contexts;
using Park2API.Entities;

namespace Park2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SlotsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Slots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Slot>>> GetSlots()
        {
            return await _context.Slots.ToListAsync();
        }

        // GET: api/Slots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Slot>> GetSlot(int id)
        {
            var slot = await _context.Slots.FindAsync(id);

            if (slot == null)
            {
                return NotFound();
            }

            return slot;
        }

        // GET: api/Slots/availability/{start}/{end}
        [HttpGet]
        [Route("~/api/slots/availability/{start}/{end}")]
        //public async Task<ActionResult<IEnumerable<Slot>>> GetSlots(DateTime start, DateTime end)
        public async Task<ActionResult<IEnumerable<Slot>>> GetAvailableSlots(DateTime start, DateTime end)
        {
            var reservations = _context.Reservations.Include(s => s.Slot);
            List<Slot> freeslots = new List<Slot>();

            freeslots = _context.Slots.Where(x => x.Status == "Available").ToList();
            foreach (var item in reservations)
            {
                if ((item.TimeStart <= start & item.TimeEnd > start) ||
                    (item.TimeStart < end & item.TimeEnd >= end) ||
                    (item.TimeStart <= start & item.TimeEnd >= end) ||
                    ((item.TimeStart >= start & item.TimeEnd <= end)))
                {
                    var slotToRemove = item.Slot;
                    freeslots.Remove(slotToRemove);
                }
            }
            return freeslots;
        }

        // PUT: api/Slots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlot(int id, Slot slot)
        {
            if (id != slot.Id)
            {
                return BadRequest();
            }

            _context.Entry(slot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Slots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Slot>> PostSlot(Slot slot)
        {
            _context.Slots.Add(slot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSlot", new { id = slot.Id }, slot);
        }

        // DELETE: api/Slots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlot(int id)
        {
            var slot = await _context.Slots.FindAsync(id);
            if (slot == null)
            {
                return NotFound();
            }

            _context.Slots.Remove(slot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SlotExists(int id)
        {
            return _context.Slots.Any(e => e.Id == id);
        }
    }
}
