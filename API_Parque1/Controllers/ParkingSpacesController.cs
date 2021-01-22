using API_Parque1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_Parque1.Controllers
{
    public class ParkingSpacesController : ApiController
    {

        List<ParkingSpace> parkingSpaces = new List<ParkingSpace>();

        public ParkingSpacesController() 
        {
            parkingSpaces.Add(new ParkingSpace { cod_ps = 1, name_ps = "A01", section_ps = 'A', is_available = false });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 2, name_ps = "A02", section_ps = 'A' });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 3, name_ps = "A03", section_ps = 'A' });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 4, name_ps = "A04", section_ps = 'A', is_available = false });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 5, name_ps = "A05", section_ps = 'A', is_available = false });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 6, name_ps = "A06", section_ps = 'A', is_available = false });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 7, name_ps = "A07", section_ps = 'A', is_available = false });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 8, name_ps = "A08", section_ps = 'A' });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 9, name_ps = "A09", section_ps = 'A' });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 10, name_ps = "A10", section_ps = 'A' });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 11, name_ps = "B01", section_ps = 'B' });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 12, name_ps = "B02", section_ps = 'B' });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 13, name_ps = "B03", section_ps = 'B', is_available = false });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 14, name_ps = "B04", section_ps = 'B' });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 15, name_ps = "B05", section_ps = 'B' });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 16, name_ps = "B06", section_ps = 'B' });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 17, name_ps = "B07", section_ps = 'B' });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 18, name_ps = "B08", section_ps = 'B' });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 19, name_ps = "B09", section_ps = 'B', is_available = false });
            parkingSpaces.Add(new ParkingSpace { cod_ps = 20, name_ps = "B10", section_ps = 'B', is_available = false });

        }

        //GET: api/ParkingSpaces
        public List<ParkingSpace> Get()
        {
            return parkingSpaces;
        }

        //GET: api/ParkingSpaces/{cod_ps}
        public ParkingSpace Get(int id)
        {
            return parkingSpaces.Where(x => x.cod_ps == id).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>This returns the list of all parking space names.</returns>
        [Route("api/ParkingSpaces/GetParkingSpaceNames")]
        [HttpGet]
        public List<string> GetParkingSpaceNames() 
        {
            List<string> output = new List<string>();
        
            foreach (var space in parkingSpaces) 
            {
                output.Add(space.name_ps);
            }

            return output;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>This returns a list of available parking spaces.</returns>
        [Route("api/ParkingSpaces/GetAvailableParkingSpaces")]
        [HttpGet]
        public List<string> GetAvailableParkingSpaces()
        {
            List<string> output = new List<string>();

            foreach (var space in parkingSpaces)
            {
                if (space.is_available == true)
                output.Add(space.name_ps);
            }

            return output;
        }

        //POST: api/ParkingSpaces
        public void Post(ParkingSpace val)
        {
            parkingSpaces.Add(val);
        }

        //DELETE: api/ParkingSpaces/{cod_ps}
        public void Delete(int id)
        {

        }


    }
}
