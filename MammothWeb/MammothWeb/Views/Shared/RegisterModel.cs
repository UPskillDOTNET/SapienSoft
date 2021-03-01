using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MammothWeb.Models
{
    public class RegisterModel
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage="Fisrt Name is required.")]
        [MinLength(4)]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last Name is required.")]
        [MinLength(4)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [MinLength(4)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Not a valid Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8)]
        public string Password { get; set; }

        [Display(Name = "Payment Method")]
        [Required(ErrorMessage = "Payment Method is required.")]
        public int PaymentMethodId { get; set; }
    }
}
