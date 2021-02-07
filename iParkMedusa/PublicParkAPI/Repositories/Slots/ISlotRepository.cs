using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Repositories.Slots
{
    public interface ISlotRepository
    {
        Task<IEnumerable<Slot>> GetAllSlotsAsync();
        Task<Slot> GetSlotByIdAsync(int id);
        Task<int> DeleteSlotByIdAsync(int id);
        Task<int> UpdateSlotAsync(Slot slot);
    }
}
