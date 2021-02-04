using Microsoft.AspNetCore.Identity;
using PublicParkAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NIF { get; set; }
        public string Address { get; set; }
    }
}
