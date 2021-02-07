using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Repositories
{
    public interface ISlotRepository : IBaseRepository<Slot>
    {
        Task<IEnumerable<Slot>> GetAllSlotsAsync();
        Task<Slot> GetSlotByIdAsync(int id);
        Task<List<Slot>> GetSlotsByStatus(string status);
        Task<int> DeleteSlotByIdAsync(int id);
    }
}
