using iParkMedusa.Entities;
using iParkMedusa.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
