using iParkMedusa.Entities;
using iParkMedusa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    [Route("api/[controller]")]
    [ApiController]
    public class ParksController : ControllerBase
    {
        private readonly ParkService _service;

        public ParksController(ParkService service)
        {
            _service = service;
        }

        // GET: api/Parks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Park>>> GetParks()
        {
            try
            {
                var parks = await _service.FindAll();
                return Ok(parks);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // GET: api/Parks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Park>> GetPark(int id)
        {
            try
            {
                var park = await _service.GetParkById(id);
                if (park == null)
                {
                    return NotFound();
                }
                return Ok(park);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // PUT: api/Parks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPark(int id, Park park)
        {
            if (id != park.Id)
            {
                return BadRequest();
            }
            try
            {
                await _service.UpdatePark(park);
                return Ok(park);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }

        }

        // POST: api/Parks
        [HttpPost]
        public async Task<ActionResult<Park>> PostPark(Park park)
        {
            try
            {
                await _service.AddPark(park);
                return CreatedAtAction("GetSlot", new { id = park.Id }, park); ;
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // DELETE: api/Parks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePark(int id)
        {
            try
            {
                await _service.DeleteParkbyId(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }
    }
}
