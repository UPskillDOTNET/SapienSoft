using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<int> AddEntityAsync(T entity);
        Task<int> UpdateEntityAsync(T entity);
    }
}
