using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaxAPI.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> FindAllAsync();
        Task<int> AddEntityAsync(T entity);
        Task<int> UpdateEntityAsync(T entity);
    }
}
