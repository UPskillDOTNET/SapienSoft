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
    public class ReservationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
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



        

        // GET: api/Reservations/available/{start}/{end}
        [HttpGet]
        [Route("~/api/reservations/available/{start}/{end}")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAvailableReservations(DateTime start, DateTime end)
        {

            List<ReservationDTO> listReservations = new List<ReservationDTO>();

            var activeReservations = _context.Reservations.Include(s => s.Slot);

            var freeslots = _context.Slots.Where(x => x.Status == "Available").ToList();

            foreach (var item in activeReservations)
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

            foreach (var item in freeslots)
            {
                decimal value = 0;
                for (DateTime dt = start; dt <= end; dt = dt.AddHours(1))
                {
                    int weekDay = (int)dt.DayOfWeek;
                    var x = _context.DailyDiscounts.FirstOrDefault(d => d.TimeDivision == "Monday");
                    var discount = x.Rate;
                    value += item.PricePerHour * discount;
                }
                var reservationToAdd = new ReservationDTO() {SlotId = item.Id, TimeStart =start, TimeEnd=end, DateCreated=DateTime.Now, Value=value};
                listReservations.Add(reservationToAdd);
            }

            return listReservations;
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

        // POST: api/Reservations/booking
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("~/api/reservations/booking")]
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservationBooking(Reservation reservation)
        {
            // Confirmar que a reserva é válida e que ainda se encontra disponível
            var slot = _context.Slots.Where(x => x.Id == reservation.SlotId);
            var dbReservations = _context.Reservations.Where(s => s.SlotId == reservation.SlotId).Include(s => s.Slot);
            foreach (var item in dbReservations)
            {
                if ((item.TimeStart <= reservation.TimeStart && item.TimeEnd > reservation.TimeStart) ||
                    (item.TimeStart < reservation.TimeEnd && item.TimeEnd >= reservation.TimeEnd) ||
                    (item.TimeStart <= reservation.TimeStart && item.TimeEnd >= reservation.TimeEnd) ||
                    (item.TimeStart >= reservation.TimeStart && item.TimeEnd <= reservation.TimeEnd))
                {
                    return Ok($"The Slot id {reservation.SlotId } has a conflict reservation id {item.Id}.");
                }
            }

            var hours = (reservation.TimeEnd - reservation.TimeStart).Hours;
            var value = hours * reservation.Slot.PricePerHour;
            reservation.Value = value;
            reservation.DateCreated = DateTime.Now;

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
