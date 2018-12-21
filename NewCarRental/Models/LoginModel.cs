using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewCarRental.Models
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }       
    }
}