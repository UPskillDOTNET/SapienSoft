using iParkMedusa.Entities;
using iParkMedusa.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Services
{
    public class ParkService
    {
        private readonly IParkRepository _repo;

        public ParkService(IParkRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Park>> FindAll()
        {
            return await _repo.FindAllAsync();
        }

        public async Task<Park> GetParkById(int id)
        {
            return await _repo.GetParkByIdAsync(id);
        }

        public async Task<int> UpdatePark(Park park)
        {
            return await _repo.UpdateEntityAsync(park);
        }

        public async Task<int> AddPark(Park park)
        {
            return await _repo.AddEntityAsync(park);
        }

        public async Task<int> DeleteParkbyId(int id)
        {
            return await _repo.DeleteParkByIdAsync(id);
        }
    }
}
