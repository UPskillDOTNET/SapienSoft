using ParkAPI.Entities;
using System.Threading.Tasks;

namespace ParkAPI.Repositories
{
    public interface IStatusRepository : IBaseRepository<Status>
    {
        Task<Status> GetStatusByIdAsync(int id);
        Task<int> DeleteStatusByIdAsync(int id);
    }
}
