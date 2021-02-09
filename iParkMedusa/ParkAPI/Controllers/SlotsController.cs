using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkAPI.Entities;
using ParkAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkAPI.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    [Route("api/[controller]")]
    [ApiController]
    public class SlotsController : ControllerBase
    {
        private readonly SlotService _service;

        public SlotsController(SlotService service)
        {
            _service = service;
        }

        // GET: api/Slots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Slot>>> GetSlots()
        {
            try
            {
                var slots = await _service.GetAllSlots();
                return Ok(slots);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // GET: api/Slots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Slot>> GetSlot(int id)
        {
            try
            {
                var slot = await _service.GetSlotById(id);
                if (slot == null)
                {
                    return NotFound();
                }
                return Ok(slot);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // PUT: api/Slots/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlot(int id, Slot slot)
        {
            if (id != slot.Id)
            {
                return BadRequest();
            }
            try
            {
                await _service.UpdateSlot(slot);
                return Ok(slot);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }

        }

        // POST: api/Slots
        [HttpPost]
        public async Task<ActionResult<Slot>> PostSlot(Slot slot)
        {
            try
            {
                await _service.AddSlot(slot);
                return CreatedAtAction("GetSlot", new { id = slot.Id }, slot); ;
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // DELETE: api/Slots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlot(int id)
        {
            try
            {
                await _service.DeleteSlotbyId(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }
    }
}
