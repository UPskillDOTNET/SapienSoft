using PaxAPI.Entities;
using PaxAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaxAPI.Services
{
    public class StatusService
    {
        private readonly IStatusRepository _repo;

        public StatusService(IStatusRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Status>> FindAll()
        {
            return await _repo.FindAllAsync();
        }

        public async Task<Status> GetStatusById(int id)
        {
            return await _repo.GetStatusByIdAsync(id);
        }

        public async Task<int> UpdateStatus(Status slot)
        {
            return await _repo.UpdateEntityAsync(slot);
        }

        public async Task<int> AddStatus(Status slot)
        {
            return await _repo.AddEntityAsync(slot);
        }

        public async Task<int> DeleteStatusbyId(int id)
        {
            return await _repo.DeleteStatusByIdAsync(id);
        }
    }
}
