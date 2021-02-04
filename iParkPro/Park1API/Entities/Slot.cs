﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Entities
{
    public class Slot
    {
        public int Id { get; set; }
        public string Locator { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool ECharging { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        public decimal PricePerHour { get; set; }

        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public SlotStatus Status { get; set; }
    }
}
