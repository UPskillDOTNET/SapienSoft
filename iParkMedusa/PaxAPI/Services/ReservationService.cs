using PaxAPI.Entities;
using PaxAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaxAPI.Constants;

namespace PaxAPI.Services
{
    public class ReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ISlotRepository _slotRepository;

        public ReservationService(IReservationRepository reservationRepository, ISlotRepository slotRepository)
        {
            _reservationRepository = reservationRepository;
            _slotRepository = slotRepository;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationRepository.GetAllReservationsAsync();
        }

        public async Task<Reservation> GetReservationById(int id)
        {
            return await _reservationRepository.GetReservationByIdAsync(id);
        }

        public async Task<int> UpdateReservation(Reservation reservation)
        {
            return await _reservationRepository.UpdateEntityAsync(reservation);
        }

        public async Task<int> AddReservation(Reservation reservation)
        {
            return await _reservationRepository.AddEntityAsync(reservation);
        }

        public async Task<int> DeleteReservationById(int id)
        {
            return await _reservationRepository.DeleteReservationByIdAsync(id);
        }

        public async Task<List<ReservationDTO>> GetAvailableSlotsToReservationDTO(DateTime start, DateTime end, string userId)
        {
            List<ReservationDTO> listReservationsDTO = new List<ReservationDTO>();
            var reservations = await _reservationRepository.GetAllReservationsAsync();
            var slots = await _slotRepository.GetSlotsByStatus("Available");

            // Removes Slots that are not available
            foreach (var item in reservations)
            {
                if ((item.Start <= start & item.End > start) ||
                    (item.Start < end & item.End >= end) ||
                    (item.Start <= start & item.End >= end) ||
                    ((item.Start >= start & item.End <= end)))
                {
                    var slotToRemove = item.Slot;
                    slots.Remove(slotToRemove);
                }
            }

            foreach (var item in slots)
            {
                var value = item.PricePerHour * (end - start).TotalHours;

                ReservationDTO reservationDTO = new ReservationDTO()
                {
                    ParkName = Globals.ParkName,
                    Latitude = Globals.Latitude,
                    Longitude = Globals.Longitude,
                    Address = Globals.Address,
                    DateCreated = DateTime.Now,
                    Start = start,
                    End = end,
                    SlotId = item.Id,
                    Locator = item.Locator,
                    ChargingOption = item.IsChargingAvailable,
                    ValletOption = item.HasVallet,
                    CoverOption = item.IsCovered,
                    OutsideOption = item.IsOutside,
                    Value = value,
                    UserId = userId
                };
                listReservationsDTO.Add(reservationDTO);
            }
            return listReservationsDTO;
        }

        public async Task<bool> IsReservationPossible(DateTime start, DateTime end, int slotId)
        {
            // Check if Reservation is possible (does not conflict with other Reservations)
            var reservations = await _reservationRepository.GetReservationsBySlotId(slotId);
            foreach (var item in reservations)
            {
                if ((item.Start <= start && item.End > start) ||
                    (item.Start < end && item.End >= end) ||
                    (item.Start <= start && item.End >= end) ||
                    (item.Start >= start && item.End <= end))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<ReservationDTO> CreateNewReservation(DateTime start, DateTime end, int slotId, string userId)
        {
            var slot = await _slotRepository.GetSlotByIdAsync(slotId);

            Reservation reservation = new Reservation()
            {
                Start = start,
                End = end,
                DateCreated = DateTime.Now,
                Value = (end - start).TotalHours * slot.PricePerHour,
                SlotId = slotId,
                UserId = userId
            };
            await _reservationRepository.AddEntityAsync(reservation);
            
            var id = reservation.Id;
            ReservationDTO reservationDTO = new ReservationDTO()
            {

                ParkName = Globals.ParkName,
                Latitude = Globals.Latitude,
                Longitude = Globals.Longitude,
                Address = Globals.Address,
                DateCreated = DateTime.Now,
                Start = start,
                End = end,
                SlotId = slotId,
                Locator = slot.Locator,
                ChargingOption = slot.IsChargingAvailable,
                ValletOption = slot.HasVallet,
                CoverOption = slot.IsCovered,
                OutsideOption = slot.IsOutside,
                Value = (end - start).TotalHours * slot.PricePerHour,
                UserId = userId,
                ExternalId = reservation.Id
            };
            return reservationDTO;
        }
    }
}
