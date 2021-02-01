using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iParkCentralAPI.Contexts;
using iParkCentralAPI.Entities;

namespace iParkCentralAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ParksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Parks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Park>>> GetPark()
        {
            return await _context.Park.ToListAsync();
        }

        // GET: api/Parks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Park>> GetPark(int id)
        {
            var park = await _context.Park.FindAsync(id);

            if (park == null)
            {
                return NotFound();
            }

            return park;
        }

        // PUT: api/Parks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPark(int id, Park park)
        {
            if (id != park.Id)
            {
                return BadRequest();
            }

            _context.Entry(park).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkExists(id))
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

        // POST: api/Parks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Park>> PostPark(Park park)
        {
            _context.Park.Add(park);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPark", new { id = park.Id }, park);
        }

        // DELETE: api/Parks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePark(int id)
        {
            var park = await _context.Park.FindAsync(id);
            if (park == null)
            {
                return NotFound();
            }

            _context.Park.Remove(park);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParkExists(int id)
        {
            return _context.Park.Any(e => e.Id == id);
        }
    }
}
