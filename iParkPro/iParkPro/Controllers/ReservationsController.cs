using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iParkPro.Contexts;
using iParkPro.Entities;
using iParkPro.Services;
using Microsoft.AspNetCore.Identity;
using iParkPro.Models;

namespace iParkPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IParkService _parkService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservationsController(ApplicationDbContext context, IParkService parkService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _parkService = parkService;
            _userManager = userManager;
        }


        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.ToListAsync();
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }


        // GET: api/Reservations/Available?start=...&end=...
        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetAvailableReservations([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            List<Reservation> listReservations = new List<Reservation>();

            var userName = _userManager.GetUserId(HttpContext.User);
            
            ApplicationUser user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            var userId = user.Id;

            Park1APIService x = new Park1APIService();
            var list1 = await x.GetAvailable(start, end, userId);
            listReservations.AddRange(list1);

            /*
            Park2APIService y = new Park2APIService();
            var list2 = await y.GetAvailable(start, end);
            listReservations.AddRange(list2);
            */

            return Ok(listReservations);

        }


        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
