using PaxAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaxAPI.Repositories
{
    public interface ISlotRepository : IBaseRepository<Slot>
    {
        Task<IEnumerable<Slot>> GetAllSlotsAsync();
        Task<Slot> GetSlotByIdAsync(int id);
        Task<List<Slot>> GetSlotsByStatus(string status);
        Task<int> DeleteSlotByIdAsync(int id);
    }
}
