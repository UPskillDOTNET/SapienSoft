using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iParkCentralAPI.Contexts;
using iParkCentralAPI.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http.Json;
using iParkCentralAPI.DTO;

namespace iParkCentralAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservation()
        {
            return await _context.Reservation.ToListAsync();
        }

        // GET: api/Reservations/Available
        [HttpGet]
        [Route("~/api/reservations/available/{start}/{end}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAvailable(DateTime start, DateTime end)
        {
            List<Reservation> reservations = new List<Reservation>();
            var parks = _context.Park;
            foreach (var item in parks)
            {

                var reservationsTemp = new List<Reservation>();
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(item.BaseUrl);

                    client.DefaultRequestHeaders.Clear();

                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    var parkUrl = item.ParkUrl(start, end);

                    try
                    {
                        //Sending request to find web api REST service resource Continents using HttpClient  
                        HttpResponseMessage Res = await client.GetAsync(parkUrl);

                        //Checking the response is successful or not which is sent using HttpClient  
                        if (Res.IsSuccessStatusCode)
                        {
                            //Storing the response details recieved from web api   
                            var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                            //Deserializing the response recieved from web api and storing into the Employee list  
                            reservationsTemp = JsonConvert.DeserializeObject<List<Reservation>>(EmpResponse);
                        }

                        reservations.AddRange(reservationsTemp);

                    }

                    catch (Exception)
                    {
                    }

                }
            }

            return reservations;
        }


        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /* POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation1(Reservation reservation)
        {
            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }
        */

        // POST: api/Reservations
        [HttpPost]
        [Route("~/api/reservations")]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {


            var reservationDTO = new ReservationDTO()
            {
                SlotId = reservation.SlotId,
                TimeStart = reservation.TimeStart,
                TimeEnd = reservation.TimeEnd,
                DateCreated = reservation.DateCreated,
                Value = reservation.Value,
                UserId = reservation.UserId,
                Latitude = reservation.Latitude,
                Longitude = reservation.Longitude
            }; 
            

            var parkId = reservation.ParkId;
            var slotId = reservation.SlotId;
            var parks = _context.Park;

            using (var client = new HttpClient())
            {
                var parkTemp = parks.FirstOrDefault(x => x.Id == parkId);

                client.BaseAddress = new Uri(parkTemp.BaseUrl);

                var postJob = client.PostAsJsonAsync<Reservation>("api/reservations", reservation);

                postJob.Wait();

                var postResult = postJob.Result;

                if (postResult.IsSuccessStatusCode)
                    return reservation;
            }

            ModelState.AddModelError(string.Empty, "Server error. Please check with your Admin");
            return Conflict("OOOOoooh MAMA! DEU ERRO");
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }
    }
}
