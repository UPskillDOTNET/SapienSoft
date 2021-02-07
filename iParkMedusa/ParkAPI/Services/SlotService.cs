using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.Entities;
using PublicParkAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Services
{
    public class SlotService
    {
        private readonly ISlotRepository _repo;

        public SlotService(ISlotRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Slot>> GetAllSlots()
        {
            return await _repo.GetAllSlotsAsync();
        }

        public async Task<Slot> GetSlotById(int id)
        {
            return await _repo.GetSlotByIdAsync(id);
        }

        public async Task<int> UpdateSlot(Slot slot)
        {
            return await _repo.UpdateEntityAsync(slot);
        }

        public async Task<int> AddSlot(Slot slot)
        {
            return await _repo.AddEntityAsync(slot);
        }

        public async Task<int> DeleteSlotbyId(int id)
        {
            return await _repo.DeleteSlotByIdAsync(id);
        }
    }
}
