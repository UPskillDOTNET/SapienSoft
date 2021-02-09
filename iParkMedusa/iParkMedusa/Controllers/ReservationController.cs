using iParkMedusa.Entities;
using iParkMedusa.Models;
using iParkMedusa.Services;
using iParkMedusa.Services.Parks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservationController(ReservationService service, UserManager<ApplicationUser> userManager)
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
                var parks = await _service.FindAll();
                return Ok(parks);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            try
            {
                var reservation = await _service.GetReservationById(id);
                if (reservation == null)
                {
                    return NotFound();
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
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAvailableSlotsToReservationDTO([FromQuery] DateTime start, [FromQuery] DateTime end)
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

            var listReservations = await _service.GetReservationsAvailable(start, end);

            /*
            Park2APIService y = new Park2APIService();
            var list2 = await y.GetAvailable(start, end);
            listReservations.AddRange(list2);
            */

            return Ok(listReservations);
        }


        // GET: api/reservations/qrcode/5
        [Authorize]
        [HttpGet]
        [Route("~/api/Reservations/qrcode/{id}")]
        public async Task<ActionResult<QrCodeModel>> GetQrCodeInformation(int id)
        {
            return await _service.GetQrCodeInformation(id);
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
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            try
            {
                await _service.AddReservation(reservation);
                return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation); ;
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // PUT: api/Reservations/Rent/5
        [HttpPut]
        [Route("~/api/Reservations/rent")]
        public async Task<ActionResult<Reservation>> RentReservation([FromQuery] int id, [FromQuery] double rentValue)
        {
            try
            {
                var updatedReservation = await _service.RentReservation(id, rentValue);
                return Ok(updatedReservation);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // PUT: api/Reservations/Rental/5
        [HttpPost]
        [Route("~/api/reservations/rented")]
        public async Task<ActionResult> RentedReservation(int reservationId)
        {
            try
            {
                var userName = _userManager.GetUserId(HttpContext.User);
                var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
                var userId = user.Id;

                var reservation = await _service.RentedReservation(reservationId, userId);

                return Ok(reservation);
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
                await _service.DeleteReservationbyId(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }
    }
}
