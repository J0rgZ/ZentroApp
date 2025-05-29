using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZentroApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}