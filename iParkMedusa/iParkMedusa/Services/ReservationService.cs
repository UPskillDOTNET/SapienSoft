using iParkMedusa.Entities;
using iParkMedusa.Models;
using iParkMedusa.Repositories;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
            reservation.QrCode = "https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=http://localhost:44398/api/reservations/qrcode/" + reservation.Id;
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
                QrCode = reservation.QrCode,
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
            reservation.Value = 0;
            await _reservationRepo.UpdateEntityAsync(reservation);
            return reservation;
        }

        public  Reservation ReservationDTO2Reservation (ReservationDTO reservationAPI, int parkId, string userId)
        {
            Reservation newReservation = new Reservation()
            {
                ExternalId = reservationAPI.Locator,
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
    }
}
