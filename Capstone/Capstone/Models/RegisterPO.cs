using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class RegisterPO
    {
        [Required(ErrorMessage = "Field cannot be empty, please enter valid username.")]
        [StringLength(32, MinimumLength = 6)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Field cannot be empty, please enter valid password.")]
        [StringLength(64, MinimumLength = 8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field cannot be empty, please enter valid email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(64)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field cannot be empty, please enter your name.")]
        [StringLength(16)]
        public string Name { get; set; }
    }
}