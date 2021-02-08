using iParkMedusa.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Repositories
{
    public interface IParkRepository : IBaseRepository<Park>
    {
        Task<Park> GetParkByIdAsync(int id);
        Task<int> DeleteParkByIdAsync(int id);
    }
}
