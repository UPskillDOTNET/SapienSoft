using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.Entities;
using PublicParkAPI.Repositories.Slots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Services.Slots
{
    public class SlotService
    {
        private readonly ISlotRepository _repo;

        public SlotService(ISlotRepository repo)
        {
            _repo = repo;
        }

        public async Task<ActionResult<IEnumerable<Slot>>> GetAllSlots()
        {
            return await _repo.GetAllSlotsAsync();
        }

        public async Task<ActionResult<Slot>> GetSlotById(int id)
        {
            return await _repo.GetSlotById(id);
        }
    }
}
