using Microsoft.AspNetCore.Mvc;
using PaxAPI.Entities;
using PaxAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly StatusService _service;

        public StatusController(StatusService service)
        {
            _service = service;
        }

        // GET: api/Status
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatus()
        {
            try
            {
                var Status = await _service.FindAll();
                return Ok(Status);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // GET: api/Status/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetSlot(int id)
        {
            try
            {
                var status = await _service.GetStatusById(id);
                if (status == null)
                {
                    return NotFound();
                }
                return Ok(status);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // PUT: api/Status/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(int id, Status status)
        {
            if (id != status.Id)
            {
                return BadRequest();
            }
            try
            {
                await _service.UpdateStatus(status);
                return Ok(status);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }

        }

        // POST: api/Status
        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
            try
            {
                await _service.AddStatus(status);
                return CreatedAtAction("GetStatus", new { id = status.Id }, status); ;
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // DELETE: api/Status/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            try
            {
                await _service.DeleteStatusbyId(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }
    }
}
