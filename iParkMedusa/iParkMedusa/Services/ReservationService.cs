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
        private readonly IReservationRepository _repo;
        

        public ReservationService(IReservationRepository repo)
        {
            _repo = repo;
            
        }

        public async Task<IEnumerable<Reservation>> FindAll()
        {
            return await _repo.FindAllAsync();
        }

        public async Task<Reservation> GetReservationById(int id)
        {
            return await _repo.GetReservationByIdAsync(id);
        }

        public async Task<int> UpdateReservation(Reservation reservation)
        {
            return await _repo.UpdateEntityAsync(reservation);
        }

        public async Task<int> AddReservation(Reservation reservation)
        {
            

            return await _repo.AddEntityAsync(reservation);
        }

        public async Task<int> DeleteReservationbyId(int id)
        {
            return await _repo.DeleteReservationByIdAsync(id);
        }

       
        public async Task<QrCodeModel> GetQrCodeInformation(int id)
        {
            var reservation = await _repo.GetReservationByIdAsync(id);
         
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
            var reservation = await _repo.GetReservationByIdAsync(reservationId);
            reservation.AvailableToRent = true;
            reservation.RentValue = rentValue;
            await _repo.UpdateEntityAsync(reservation);
            return reservation;
        }

        public async Task<Reservation> RentedReservation(int reservationId, string userId)
        {
            var reservation = await _repo.GetReservationByIdAsync(reservationId);
            reservation.UserId = userId;
            reservation.AvailableToRent = false;
            reservation.Value = null;
            await _repo.UpdateEntityAsync(reservation);
            return reservation;
        }
        public  Reservation ReservationDTo2Reservation (ReservationDTO reservationAPI, int id/*, string loggedUserId*/)
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
                /*UserId = loggedUserId,*/
                ParkId = id
            };
            return newReservation;
        }
    }
}
