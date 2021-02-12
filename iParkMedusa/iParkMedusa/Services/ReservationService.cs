using iParkMedusa.Entities;
using iParkMedusa.Models;
using iParkMedusa.Repositories;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace iParkMedusa.Services
{
    public class ReservationService
    {
        private readonly IReservationRepository _reservationRepo;
        private readonly ITransactionRepository _transactionRepo;
        
        public ReservationService(IReservationRepository reservationRepo, ITransactionRepository transactionRepo)
        {
            _reservationRepo = reservationRepo;
            _transactionRepo = transactionRepo;
            
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationRepo.GetAllReservationsAsync();
        }

        public async Task<Reservation> GetReservationById(int id)
        {
            return await _reservationRepo.GetReservationByIdAsync(id);
        }

        public async Task<int> UpdateReservation(Reservation reservation)
        {
            return await _reservationRepo.UpdateEntityAsync(reservation);
        }

        public async Task<int> AddReservation(Reservation reservation)
        {
            return await _reservationRepo.AddEntityAsync(reservation);
        }

        public async Task<int> DeleteReservationbyId(int id)
        {
            
            return await _reservationRepo.DeleteReservationByIdAsync(id);
        }

        public Reservation GenerateQrCode(Reservation reservation)
        {
            reservation.QrCode = "https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=https://localhost:44398/api/reservations/qrcode/" + reservation.Id;
            return reservation;
        }

        public async Task<QrCodeModel> GetQrCodeInformation(int id)
        {
            var reservation = await _reservationRepo.GetReservationByIdAsync(id);
         
            var qrCodeInformation = new QrCodeModel()
            {
                Id = reservation.Id,
                ExternalId = reservation.ExternalId,
                Start = reservation.Start,
                End = reservation.End,
                Latitude = reservation.Latitude,
                Longitude = reservation.Longitude,
                UserFirstName = reservation.ApplicationUser.FirstName,
                UserLastName = reservation.ApplicationUser.LastName,
                ParkName = reservation.Park.Name
            };
            return qrCodeInformation;
        }

        public async Task<Reservation> RentReservation(int reservationId, double rentValue)
        {
            var reservation = await _reservationRepo.GetReservationByIdAsync(reservationId);
            reservation.AvailableToRent = true;
            reservation.RentValue = rentValue;
            await _reservationRepo.UpdateEntityAsync(reservation);
            return reservation;
        }

        public async Task<Reservation> RentedReservation(int reservationId, string userId)
        {
            var reservation = await _reservationRepo.GetReservationByIdAsync(reservationId);

            reservation.UserId = userId;
            reservation.AvailableToRent = false;
            reservation.RentValue = 0;
            await _reservationRepo.UpdateEntityAsync(reservation);
            return reservation;
        }

        public  Reservation ReservationDTO2Reservation (ReservationDTO reservationAPI, int parkId, string userId)
        {
            Reservation newReservation = new Reservation()
            {
                ExternalId = reservationAPI.ExternalId,
                Start = reservationAPI.Start,
                End = reservationAPI.End,
                DateCreated = DateTime.Now,
                Value = reservationAPI.Value,
                SlotId = reservationAPI.SlotId,
                Locator = reservationAPI.Locator,
                Latitude = reservationAPI.Latitude,
                Longitude = reservationAPI.Longitude,
                UserId = userId,
                ParkId = parkId
            };
            return newReservation;
        }

        public async Task<bool> UserHasBalance(ApplicationUser user, double value)
        {
            var userBalance = await _transactionRepo.GetBalanceByUserIdAsync(user.Id);
            if (userBalance >= value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<List<Reservation>> GetRentReservations(DateTime start, DateTime end)
        {
            return await _reservationRepo.GetRentReservationsAsync(start, end);
        }


        public ReservationDTO Reservation2ReservationDTO(Reservation reservation)
        {
            ReservationDTO newReservationDTO = new ReservationDTO()
            {
                Start = reservation.Start,
                End = reservation.End,
                DateCreated = reservation.DateCreated,
                Value = reservation.RentValue,
                ExternalId = reservation.ExternalId,
                SlotId = reservation.SlotId,
                Locator = reservation.Locator,
                Latitude = reservation.Latitude,
                Longitude = reservation.Longitude,
                UserId = reservation.UserId,
                
            };
            return newReservationDTO;
        }
        public void SendEmail(Reservation newReservation, ApplicationUser user, string parkName)
        {
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
                                                <p>Here's your ticket:</p>
                                                <p><img src=""cid:myimage"" /></p>
                                                <p>Name: "+user.FirstName+" "+user.LastName+@"</p>
                                                <p>Park: "+parkName+@"</p>
                                                <p>Coordinates: "+newReservation.Latitude.ToString("N6")+", "+newReservation.Longitude.ToString("N6") + @"</p>
                                                <p>Locator: "+newReservation.Locator+@"</p>
                                                <p>Start: "+newReservation.Start.ToShortDateString()+" "+newReservation.Start.ToShortTimeString()+@"</p>
                                                <p>End: "+newReservation.End.ToShortDateString()+" "+newReservation.End.ToShortTimeString()+@"</p>
                                                <p>Value: "+newReservation.Value+@" €</p>
                                                <p> ~ The SapienSoft Team </p>
                                            </body>
                                        </html>
                                        ";

                AlternateView view = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                view.LinkedResources.Add(image);

                mail.IsBodyHtml = true;
                mail.AlternateViews.Add(view);
                mail.From = new MailAddress("sapiensoft.upskill@gmail.com");
                mail.To.Add(user.Email);
                mail.Subject = "Reservation " + newReservation.Id + " is completed";

                sender.Send(mail);
            }
        }
    }
}
