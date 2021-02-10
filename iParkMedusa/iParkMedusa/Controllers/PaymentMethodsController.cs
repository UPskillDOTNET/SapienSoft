using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iParkMedusa.Contexts;
using iParkMedusa.Entities;
using Microsoft.AspNetCore.Authorization;
using iParkMedusa.Services;

namespace iParkMedusa.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly PaymentMethodService _service;

        public PaymentMethodsController(PaymentMethodService service)
        {
            _service = service;
        }

        // GET: api/PaymentMethods
        [HttpGet]
        public async Task<ActionResult<List<PaymentMethod>>> GetPaymentMethods()
        {
            try
            {
                var paymentMethods = await _service.FindAll();
                return Ok(paymentMethods);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // GET: api/PaymentMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethod(int id)
        {
            try
            {
                var paymentMethod = await _service.GetPaymentMethodById(id);
                if (paymentMethod == null)
                {
                    return NotFound();
                }
                return Ok(paymentMethod);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // PUT: api/PaymentMethods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentMethod(int id, PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.Id)
            {
                return BadRequest();
            }
            try
            {
                await _service.UpdatePaymentMethod(paymentMethod);
                return Ok(paymentMethod);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }

        }

        // POST: api/PaymentMethods
        [HttpPost]
        public async Task<ActionResult<PaymentMethod>> PostPaymentMethod(PaymentMethod paymentMethod)
        {
            try
            {
                await _service.AddPaymentMethod(paymentMethod);
                return CreatedAtAction("GetSlot", new { id = paymentMethod.Id }, paymentMethod); ;
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // DELETE: api/PaymentMethods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            try
            {
                await _service.DeletePaymentMethodById(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }
    }
}
