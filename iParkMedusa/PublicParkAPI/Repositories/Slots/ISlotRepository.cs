using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Repositories.Slots
{
    public interface ISlotRepository : IBaseRepository<Slot>
    {
        Task<ActionResult<IEnumerable<Slot>>> GetAllSlotsAsync();
        Task<ActionResult<Slot>> GetSlotById(int id);
    }
}
