using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Park2API.Contexts;
using Park2API.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Park2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Slots1Controller : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public Slots1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Slots1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Slot>>> GetSlots()
        {
            return await _context.Slots.ToListAsync();
        }

        // GET: api/Slots1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Slot>> GetSlot(string id)
        {
            var slot = await _context.Slots.FindAsync(id);

            if (slot == null)
            {
                return NotFound();
            }

            return slot;
        }

        // PUT: api/Slots1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlot(string id, Slot slot)
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

        // POST: api/Slots1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Slot>> PostSlot(Slot slot)
        {
            _context.Slots.Add(slot);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SlotExists(slot.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSlot", new { id = slot.Id }, slot);
        }

        // DELETE: api/Slots1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlot(string id)
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

        private bool SlotExists(string id)
        {
            return _context.Slots.Any(e => e.Id == id);
        }
    }
}
