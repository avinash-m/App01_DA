using System.ComponentModel.DataAnnotations;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username {get; set;}
        [Required]
        //[StringLengthAttribute(3)]
        public string Password {get; set; }
    }
}