using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParkAPI.Entities;
using ParkAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservationsController(ReservationService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            try
            {
                var reservation = await _service.GetAllReservations();
                return Ok(reservation);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // GET: api/Reservations/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Slot>> GetReservation(int id)
        {
            try
            {
                var reservation = await _service.GetReservationById(id);
                if (reservation == null)
                {
                    return NotFound();
                }

                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (User.IsInRole("User") && (reservation.UserId != user.Id))
                {
                    return Unauthorized();
                }

                return Ok(reservation);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // GET: api/Reservations/Available?start=...&end=...
        [Authorize(Roles = "Administrator, Moderator, User")]
        [HttpGet]
        [Route("~/api/reservations/available")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAvailableSlotsToReservationDTO([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            // Dates validation (DateTime default value "0001-01-01 00:00:00")
            if (start > end)
            {
                return BadRequest($"DateTime 'end' ({end}) must be greater than DateTime 'start' ({start}).");
            }
            else if (start < DateTime.Now)
            {
                return BadRequest($"DateTime 'start' ({start}) must happen in the future.");
            }

            // Get active User
            var userName = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            var userId = user.Id;

            try
            {
                var listReservationsDTO = await _service.GetAvailableSlotsToReservationDTO(start, end, userId);
                return Ok(listReservationsDTO);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }
            try
            {
                await _service.UpdateReservation(reservation);
                return Ok(reservation);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // POST: api/Reservations
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            try
            {
                await _service.AddReservation(reservation);
                return CreatedAtAction("GetSlot", new { id = reservation.Id }, reservation); ;
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }


        // POST: api/Reservations/booking
        [Authorize(Roles = "User")]
        [Route("~/api/reservations/booking")]
        [HttpPost]
        public async Task<ActionResult<ReservationDTO>> PostReservationBooking(ReservationDTO reservation)
        {
            var userName = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            var userId = user.Id;

            try
            {
                var isReservationPossible = await _service.IsReservationPossible(reservation.Start, reservation.End, reservation.SlotId);
                if (!isReservationPossible)
                {
                    return BadRequest($"The Slot id {reservation.SlotId} has a conflict. Reservation not valid.");
                }
                var reservationDTO = await _service.CreateNewReservation(reservation.Start, reservation.End, reservation.SlotId, userId);
                return Ok(reservationDTO);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            try
            {
                await _service.DeleteReservationById(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }              

    }
}
