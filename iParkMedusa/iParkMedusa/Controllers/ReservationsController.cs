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
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace iParkMedusa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationService _service;
        private readonly ParkAPIService _parkAPIService;
        private readonly PaxAPIService _paxAPIService;
        private readonly ParkService _parkService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TransactionService _transactionService;

        public ReservationsController(ReservationService service, ParkAPIService parkAPIService, PaxAPIService paxAPIService, ParkService parkService, UserManager<ApplicationUser> userManager, TransactionService transactionService)
        {
            _service = service;
            _parkAPIService = parkAPIService;
            _paxAPIService = paxAPIService;
            _parkService = parkService;
            _userManager = userManager;
            _transactionService = transactionService;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            try
            {
                var parks = await _service.GetAllReservations();
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

            try
            {
                var listReservations = await _parkAPIService.GetAvailable(start, end); // GetAvailable from ParkAPI
                listReservations.AddRange(await _paxAPIService.GetAvailable(start, end)); // Add from PaxAPI
                var listRentReservations = await _service.GetRentReservations(start, end); // Add the Rent reservations
                var listRentReservationsDTO = new List<ReservationDTO>();
                foreach ( var item in listRentReservations)
                {
                     var DTO = _service.Reservation2ReservationDTO(item);
                    listRentReservationsDTO.Add(DTO);
                }
                
                listReservations.AddRange(listRentReservationsDTO);
                return Ok(listReservations);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }


        // GET: api/reservations/qrcode/5
        [Authorize]
        [HttpGet]
        [Route("~/api/Reservations/qrcode/{reservationId}")]
        public async Task<ActionResult<QrCodeModel>> GetQrCodeInformation(int reservationId)
        {
            return await _service.GetQrCodeInformation(reservationId);
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

        // POST: api/Reservations/1
        [Authorize(Roles ="Administrator, Moderator, User")]
        [HttpPost("{idPark}")]
        public async Task<ActionResult<Reservation>> PostReservation(ReservationDTO reservation, int idPark)
        {

            if (idPark == 1)
            {
                var reservationAPI = await _parkAPIService.PostReservation(reservation.Start, reservation.End, reservation.SlotId);

                if (reservationAPI != null)
                {
                    var userName = _userManager.GetUserId(HttpContext.User);
                    var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
                    var userId = user.Id;

                    var newReservation = _service.ReservationDTO2Reservation(reservationAPI, idPark, userId);

                    if (await _service.UserHasBalance(user, newReservation.Value))
                    {
                        newReservation = _service.GenerateQrCode(newReservation);
                        var transaction = new Transaction()
                        {
                            Value = newReservation.Value,
                            TransactionTypeId = 1
                        };
                        await _transactionService.CreateTransaction(transaction, userId);
                        await _service.AddReservation(newReservation);

                        //_service.GenerateQrCode(newReservation);

                        // send email with ticket 
                        var Park = await _parkService.GetParkById(idPark);
                        _service.SendEmail(newReservation, user, Park.Name);

                        return Ok(newReservation);
                    }
                    else
                    {
                        await _parkAPIService.CancelReservation(reservationAPI.ExternalId);
                        return StatusCode(402);
                    }
                }
                return BadRequest();
            }
            else if (idPark == 2)
            {
                var reservationAPI = await _paxAPIService.PostReservation(reservation.Start, reservation.End, reservation.SlotId);

                if (reservationAPI != null)
                {
                    var userName = _userManager.GetUserId(HttpContext.User);
                    var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
                    var userId = user.Id;

                    var newReservation = _service.ReservationDTO2Reservation(reservationAPI, idPark, userId);

                    if (await _service.UserHasBalance(user, newReservation.Value))
                    {
                        newReservation = _service.GenerateQrCode(newReservation);
                        var transaction = new Transaction()
                        {
                            Value = newReservation.Value,
                            TransactionTypeId = 1
                        };
                        await _transactionService.CreateTransaction(transaction, userId);
                        await _service.AddReservation(newReservation);

                        _service.GenerateQrCode(newReservation);

                        // send email with ticket 
                        var Park = await _parkService.GetParkById(idPark);
                        _service.SendEmail(newReservation, user, Park.Name);

                        return Ok(newReservation);
                    }
                    else
                    {
                        await _paxAPIService.CancelReservation(reservationAPI.ExternalId);
                        return StatusCode(402);
                    }
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
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
        [Authorize]
        [HttpPost]
        [Route("~/api/reservations/rented/{reservationId}")]
        public async Task<ActionResult> RentedReservation(int reservationId)
        {
            try
            {
                var userName = _userManager.GetUserId(HttpContext.User);
                var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
                var userId = user.Id;
                var temp = await _service.GetReservationById(reservationId);
                if (temp.AvailableToRent == true )
                {
                    if (userId != temp.UserId)
                    {
                        if (await _service.UserHasBalance(user, temp.RentValue))
                        {
                            var OwnerTransaction = new Transaction()
                            {
                                Value = temp.RentValue,
                                TransactionTypeId = 2
                            };
                            await _transactionService.CreateTransaction(OwnerTransaction, temp.UserId);
                            var NewOwnerTransaction = new Transaction()
                            {
                                Value = temp.RentValue,
                                TransactionTypeId = 1
                            };
                            await _transactionService.CreateTransaction(NewOwnerTransaction, userId);

                            var reservation = await _service.RentedReservation(reservationId, userId);

                            return Ok(reservation);
                        }
                        else return BadRequest("You do not possess the required funds");
                    }
                    else return BadRequest("You can't rent a reservation that already belongs to you.");
                }
                else return BadRequest("This reservation is no longer available to sub-rent.");
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }



        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id, [FromQuery]int parkId)
        {
            try
            {
                
                var reservation = await _service.GetReservationById(id);
                var transaction = new Transaction()
                {
                    Value = reservation.Value,
                    TransactionTypeId = 3
                };

                await _transactionService.CreateTransaction(transaction, reservation.UserId);

                if (parkId == 1) 
                {
                    await _parkAPIService.CancelReservation(reservation.ExternalId);
                }
                else if (parkId == 2)
                {
                    await _paxAPIService.CancelReservation(reservation.ExternalId);
                }

                await _service.DeleteReservationbyId(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }
        [Authorize]
        [Route("~/api/reservations/resend/{id}")]
        [HttpGet]
        public async Task<IActionResult> ReSendEmail( int Id)
        {
            var userName = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            var userId = user.Id;
            var reservation = await _service.GetReservationById(Id);
            if (reservation != null)
            {
                if (reservation.UserId == userId)
                {
                    if (reservation.End > DateTime.Now)
                    {
                        _service.SendEmail(reservation, user, reservation.Park.Name);

                        return Ok("E-mail has been re-sent to user " + user.UserName);

                    }
                    else return BadRequest("The reservation has ended.");
                }
                else return BadRequest("This reservation does not belong to the logged user.");
            }
            else return BadRequest("Reservation does not exist");
        }

    }
}
