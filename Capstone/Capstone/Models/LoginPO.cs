using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneDAL.Models
{
    public class LoginPO
    {
        [Required(ErrorMessage = "Field cannot be empty, please enter username.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Field cannot be empty, please enter password.")]
        public string Password { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }
    }
}
