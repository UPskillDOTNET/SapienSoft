using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OKAPI.Data;
using OKAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKAPI.Controllers
{

    [ApiController]
    public class SlotsController : ControllerBase
    {
        private IData _data;
        public SlotsController(IData idata)
        {
            _data = idata;
        }

        [HttpGet]
        [Route("api/{controller}")]
        public IActionResult GetSlots()
        {
            return Ok(_data.GetSlots());
        }

        [HttpGet]
        [Route("api/{controller}/{id}")]
        public IActionResult GetSlot(Guid id)
        {
            var slot = _data.GetSlot(id);

            if (slot != null) 
            {
                return Ok(slot);
            }

            return NotFound($"The Slot with ID of: {id} was not found");
        }

        [HttpPost]
        [Route("api/{controller}")]
        public IActionResult AddSlot(Slot slot)
        {
            _data.AddSlot(slot);
                        
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "://" + slot.Id,
                slot);
        }

        [HttpDelete]
        [Route("api/{controller}/{id}")]
        public IActionResult DeleteSlot(Guid id)
        {
            var slot = _data.GetSlot(id);

            if (slot != null) 
            {
                _data.DeleteSlot(slot);
                return Ok();
            }

            return NotFound($"The Slot with ID of: {id} was not found");
        }

        [HttpPatch]
        [Route("api/{controller}/{id}")]
        public IActionResult EditSlot(Guid id, Slot slot)
        {
            var existingSlot = _data.GetSlot(id);

            if (existingSlot != null)
            {
                slot.Id = existingSlot.Id;
                _data.EditSlot(slot);
            }

            return Ok(slot);
        }
    }

}
