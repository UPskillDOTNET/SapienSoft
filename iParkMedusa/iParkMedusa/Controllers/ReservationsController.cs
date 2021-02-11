using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
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
        private readonly IParkingLotService _parkingLotService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TransactionService _transactionService;

        public ReservationsController(ReservationService service, IParkingLotService parkingLotService, UserManager<ApplicationUser> userManager, TransactionService transactionService)
        {
            _service = service;
            _parkingLotService = parkingLotService;
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
        // [Authorize(Roles = "Administrator, Moderator, User")]
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
                var listReservations = await _parkingLotService.GetAvailable(start, end);
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
        [Route("~/api/Reservations/qrcode/{id}")]
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
                var reservationAPI = await _parkingLotService.PostReservation(reservation.Start, reservation.End, reservation.SlotId);

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

                        // send email with ticket 
                        string imageUrl = newReservation.QrCode;
                        var webClient = new WebClient();
                        byte[] imageBytes = webClient.DownloadData(imageUrl);
                        MemoryStream ms = new MemoryStream(imageBytes);
                        LinkedResource image = new LinkedResource(ms, MediaTypeNames.Image.Jpeg) { ContentId = "myimage" };

                        using (MailMessage mail = new MailMessage())
                        using (SmtpClient sender = new SmtpClient("smtp.gmail.com", 587))
                        {
                            sender.EnableSsl = true;
                            sender.Credentials = new NetworkCredential("sapiensoft.upskill@gmail.com", "Sapien123!");
                            sender.DeliveryMethod = SmtpDeliveryMethod.Network;

                            String body = @"
                                        <html>
                                            <body>
                                                <p> Here's your ticket QR code:</p>
                                                <p> <img src=""cid:myimage"" /> </p>
                                                <p> ~ The SapienSoft Team </p>
                                            </body>
                                        </html>
                                        ";

                            AlternateView view = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                            view.LinkedResources.Add(image);

                            mail.IsBodyHtml = true;
                            mail.AlternateViews.Add(view);
                            mail.From = new MailAddress("sapiensoft.upskill@gmail.com");
                            mail.To.Add("sapiensoft.upskill@gmail.com");
                            mail.Subject = "Reservation " + newReservation.Id + " is completed";

                            sender.Send(mail);
                        }

                        return Ok(newReservation);
                    }
                    else
                    {
                        await _parkingLotService.CancelReservation(reservationAPI.ExternalId);
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
                
                var reservation = await _service.GetReservationById(id);
                var transaction = new Transaction()
                {
                    Value = reservation.Value,
                    TransactionTypeId = 3
                };
                await _transactionService.CreateTransaction(transaction, reservation.UserId);
                await _parkingLotService.CancelReservation(reservation.ExternalId);
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
