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
    public class DailyDiscountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DailyDiscountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DailyDiscounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyDiscount>>> GetDailyDiscounts()
        {
            return await _context.DailyDiscounts.ToListAsync();
        }

        // GET: api/DailyDiscounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DailyDiscount>> GetDailyDiscount(DayOfWeek id)
        {
            var dailyDiscount = await _context.DailyDiscounts.FindAsync(id);

            if (dailyDiscount == null)
            {
                return NotFound();
            }

            return dailyDiscount;
        }

        // PUT: api/DailyDiscounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDailyDiscount(DayOfWeek id, DailyDiscount dailyDiscount)
        {
            if (id != dailyDiscount.DayOfTheWeek)
            {
                return BadRequest();
            }

            _context.Entry(dailyDiscount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyDiscountExists(id))
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

        // POST: api/DailyDiscounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DailyDiscount>> PostDailyDiscount(DailyDiscount dailyDiscount)
        {
            if(dailyDiscount.DayOfTheWeek < DayOfWeek.Sunday || dailyDiscount.DayOfTheWeek > DayOfWeek.Saturday)
            {
                return Conflict();
            }

            _context.DailyDiscounts.Add(dailyDiscount);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DailyDiscountExists(dailyDiscount.DayOfTheWeek))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDailyDiscount", new { id = dailyDiscount.DayOfTheWeek }, dailyDiscount);
        }

        // DELETE: api/DailyDiscounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailyDiscount(DayOfWeek id)
        {
            var dailyDiscount = await _context.DailyDiscounts.FindAsync(id);
            if (dailyDiscount == null)
            {
                return NotFound();
            }

            _context.DailyDiscounts.Remove(dailyDiscount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DailyDiscountExists(DayOfWeek id)
        {
            return _context.DailyDiscounts.Any(e => e.DayOfTheWeek == id);
        }
    }
}
