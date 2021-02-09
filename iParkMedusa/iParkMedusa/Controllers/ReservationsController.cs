using iParkMedusa.Entities;
using iParkMedusa.Models;
using iParkMedusa.Services;
using iParkMedusa.Services.ParkingLot;
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
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationService _service;
        private readonly IParkingLotService _parkingLotService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservationsController(ReservationService service, IParkingLotService parkingLotService, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _parkingLotService = parkingLotService;
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
        // [Authorize(Roles = "Administrator, Moderator, User")]
        [HttpGet]
        [Route("~/api/reservations/available")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAvailableSlotsToReservationDTO([FromQuery] DateTime start, [FromQuery] DateTime end)
        {

            var listReservations = await _parkingLotService.GetAvailable(start, end);
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

        // POST: api/Reservations/2
        [HttpPost("{id}")]
        public async Task<ActionResult<Reservation>> PostReservation(ReservationDTO reservation, int id)
        {


            if (id == 1)
            {
                var reservationAPI = await _parkingLotService.PostReservation(reservation.Start, reservation.End, reservation.SlotId);

                if (reservationAPI != null)
                {
                    //Falta associar o User ID á reserva mas nao esta a funcionar, o loggedUserId esta a entrar a null...
                   /* var userName = _userManager.GetUserId(HttpContext.User);
                    var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
                    var loggedUserId = user.Id;*/

                    var newReservation = _service.ReservationDTo2Reservation(reservationAPI, id/*, loggedUserId*/);

                    var x = await _service.AddReservation(newReservation);

                    return Ok(reservationAPI);
                }
                return BadRequest();
            } else return BadRequest();
            
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
