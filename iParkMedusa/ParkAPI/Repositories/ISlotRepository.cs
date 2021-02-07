using ParkAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkAPI.Repositories
{
    public interface ISlotRepository : IBaseRepository<Slot>
    {
        Task<Slot> GetSlotByIdAsync(int id);
        Task<List<Slot>> GetSlotsByStatus(string status);
        Task<int> DeleteSlotByIdAsync(int id);
    }
}
