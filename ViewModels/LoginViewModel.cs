using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Acest camp nu poate fi gol.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Acest camp nu poate fi gol.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
