using PaxAPI.Entities;
using System.Threading.Tasks;

namespace PaxAPI.Repositories
{
    public interface IStatusRepository : IBaseRepository<Status>
    {
        Task<Status> GetStatusByIdAsync(int id);
        Task<int> DeleteStatusByIdAsync(int id);
    }
}
