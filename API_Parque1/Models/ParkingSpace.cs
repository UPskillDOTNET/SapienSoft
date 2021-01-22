using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Parque1.Models
{

    /// <summary>
    /// Represents one parking space.
    /// </summary>
    public class ParkingSpace
    {
        /// <summary>
        /// The parking space internal ID.
        /// </summary>
        public int cod_ps { get; set; } = 0;

        /// <summary>
        /// The name of the parking space.
        /// </summary>
        public string  name_ps { get; set; }

        /// <summary>
        /// The section where the parking space is.
        /// </summary>
        public char section_ps { get; set; }

        /// <summary>
        /// It's true if the parking space is available.
        /// </summary>
        public bool is_available { get; set; } = true;

    }
}