using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Contexts;
using PublicParkAPI.Entities;

namespace PublicParkAPI.Controllers
{
    [Authorize("Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class SlotStatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SlotStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SlotStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SlotStatus>>> GetStatus()
        {
            return await _context.Status.ToListAsync();
        }

        // GET: api/SlotStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SlotStatus>> GetSlotStatus(int id)
        {
            var slotStatus = await _context.Status.FindAsync(id);

            if (slotStatus == null)
            {
                return NotFound();
            }

            return slotStatus;
        }

        // PUT: api/SlotStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlotStatus(int id, SlotStatus slotStatus)
        {
            if (id != slotStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(slotStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotStatusExists(id))
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

        // POST: api/SlotStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SlotStatus>> PostSlotStatus(SlotStatus slotStatus)
        {
            _context.Status.Add(slotStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSlotStatus", new { id = slotStatus.Id }, slotStatus);
        }

        // DELETE: api/SlotStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlotStatus(int id)
        {
            var slotStatus = await _context.Status.FindAsync(id);
            if (slotStatus == null)
            {
                return NotFound();
            }

            _context.Status.Remove(slotStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SlotStatusExists(int id)
        {
            return _context.Status.Any(e => e.Id == id);
        }
    }
}
