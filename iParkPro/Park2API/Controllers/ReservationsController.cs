using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Park2API.Contexts;
using Park2API.Entities;
using Park2API.Models;

namespace Park2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public ReservationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Reservations
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.ToListAsync();
        }

        // GET: api/Reservations/5
        [Authorize(Roles = "Administrator, Moderator, User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (User.IsInRole("Administrator") || User.IsInRole("Moderator"))
            {
                if (reservation == null)
                {
                    return NotFound();
                }
                return reservation;
            }

            if (User.IsInRole("User"))
            {
                var userName = _userManager.GetUserId(HttpContext.User);
                ApplicationUser user = _context.Users.FirstOrDefault(u => u.UserName == userName);
                var userId = user.Id;

                if (reservation.UserId == userId)
                {
                    if (reservation == null)
                    {
                        return NotFound();
                    }
                    return reservation;
                }
            }
            return Unauthorized();
        }

        // GET: api/Reservations/available?start=2021-01-01T12:00:00&end=2021-01-02T12:00:00&eCharging=false
        [Authorize(Roles = "Administrator, Moderator, User")]
        [HttpGet]
        [Route("~/api/reservations/available")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAvailableReservations([FromQuery] DateTime start, [FromQuery] DateTime end, [FromQuery] bool eCharging)
        {
            // Dates validation (DateTime default value "0001-01-01 00:00:00")
            if (start > end)
            {
                return BadRequest($"DateTime 'end' ({end}) must be greater than DateTime 'start' ({start}).");
            }
            else if (start < DateTime.Now)
            {
                return BadRequest($"DateTime 'start' ({end}) must happen in the future.");
            }
 
            List<ReservationDTO> listReservations = new List<ReservationDTO>();
            var reservations = _context.Reservations.Include(s => s.Slot).ToList();
            var freeSlots = await _context.Slots.Where(x => x.Status.Name == "Available").ToListAsync();
            
            // Removes Slots that are not available
            foreach (var item in reservations)
            {
                if ((item.Start <= start & item.End > start) ||
                    (item.Start < end & item.End >= end) ||
                    (item.Start <= start & item.End >= end) ||
                    ((item.Start >= start & item.End <= end)))
                {
                    var slotToRemove = item.Slot;
                    freeSlots.Remove(slotToRemove);
                }
            }

            // Remove Slots with eCharging = false, if eCharging = true
            if (eCharging)
            {
                freeSlots.RemoveAll(x => x.ECharging == false);
            }

            var userName = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            var userId = user.Id;

            // Calculate the Value for each Reservation
            foreach (var item in freeSlots)
            {
                decimal value = 0;
                for (DateTime dt = start; dt <= end; dt = dt.AddHours(1))
                {
                    int weekDay = (int)dt.DayOfWeek;
                    var x = _context.Discounts.FirstOrDefault(d => (int)d.WeekDay == weekDay);
                    var discount = x.Rate;
                    value += item.PricePerHour * discount;
                }

                ReservationDTO reservationToAdd = new ReservationDTO()
                {
                    Start = start,
                    End = end,
                    DateCreated = DateTime.Now,
                    Value = value,
                    SlotId = item.Id,
                    Locator = item.Locator,
                    Latitude = Globals.Latitute,
                    Longitude = Globals.Longitude,
                    ECharging = item.ECharging,
                    UserId = userId
                };

                listReservations.Add(reservationToAdd);
            }

            return listReservations;
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest($"Reservation id {reservation.Id} not found.");
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
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        // POST: api/Reservations/booking
        [Authorize(Roles = "Administrator, Moderator, User")]
        [Route("~/api/reservations/booking")]
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservationBooking(ReservationDTO ReservationDTO)
        {
            // Confirmar que a reserva é válida e que ainda se encontra disponível
            var slot = _context.Slots.FirstOrDefault(x => x.Id == ReservationDTO.SlotId);
            var dbReservations = _context.Reservations.Where(s => s.SlotId == ReservationDTO.SlotId);
            foreach (var item in dbReservations)
            {
                if ((item.Start <= ReservationDTO.Start && item.End > ReservationDTO.Start) ||
                    (item.Start < ReservationDTO.End && item.End >= ReservationDTO.End) ||
                    (item.Start <= ReservationDTO.Start && item.End >= ReservationDTO.End) ||
                    (item.Start >= ReservationDTO.Start && item.End <= ReservationDTO.End))
                {
                    return BadRequest($"The Slot id {ReservationDTO.SlotId } has a conflict. Reservation not valid.");
                }
            }

            // Calculate Value for the Reservation
            decimal value = 0;
            for (DateTime dt = ReservationDTO.Start; dt <= ReservationDTO.End; dt = dt.AddHours(1))
            {
                int weekDay = (int)dt.DayOfWeek;
                var discount = _context.Discounts.FirstOrDefault(d => (int)d.WeekDay == weekDay);
                var rate = discount.Rate;
                value += slot.PricePerHour * rate;
            }

            // Get User (authenticated)
            var userName = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            var userId = user.Id;

            Reservation reservation = new Reservation()
            {
                Start = ReservationDTO.Start,
                End = ReservationDTO.End,
                DateCreated = DateTime.Now,
                Value = value,
                SlotId = ReservationDTO.SlotId,
                UserId = userId
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            reservation.ApplicationUser = null;

            return reservation;
        }

        // DELETE: api/Reservations/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var userName = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            var userId = user.Id;

            if (User.IsInRole("Administrator") || User.IsInRole("Moderator") && reservation.Start - DateTime.Now < Globals.CancelationPolicy && reservation.UserId == userId)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            else
                return BadRequest("Can't cancel reservation, please contact support.");

        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
