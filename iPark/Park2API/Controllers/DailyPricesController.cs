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
    public class DailyPricesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DailyPricesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DailyPrices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyPrice>>> GetDailyPrices()
        {
            return await _context.DailyPrices.ToListAsync();
        }

        // GET: api/DailyPrices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DailyPrice>> GetDailyPrice(string id)
        {
            var dailyPrice = await _context.DailyPrices.FindAsync(id);

            if (dailyPrice == null)
            {
                return NotFound();
            }

            return dailyPrice;
        }

        // PUT: api/DailyPrices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDailyPrice(string id, DailyPrice dailyPrice)
        {
            if (id != dailyPrice.DayOfTheWeek)
            {
                return BadRequest();
            }

            _context.Entry(dailyPrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyPriceExists(id))
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

        // POST: api/DailyPrices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DailyPrice>> PostDailyPrice(DailyPrice dailyPrice)
        {
            _context.DailyPrices.Add(dailyPrice);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DailyPriceExists(dailyPrice.DayOfTheWeek))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDailyPrice", new { id = dailyPrice.DayOfTheWeek }, dailyPrice);
        }

        // DELETE: api/DailyPrices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailyPrice(string id)
        {
            var dailyPrice = await _context.DailyPrices.FindAsync(id);
            if (dailyPrice == null)
            {
                return NotFound();
            }

            _context.DailyPrices.Remove(dailyPrice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DailyPriceExists(string id)
        {
            return _context.DailyPrices.Any(e => e.DayOfTheWeek == id);
        }
    }
}
