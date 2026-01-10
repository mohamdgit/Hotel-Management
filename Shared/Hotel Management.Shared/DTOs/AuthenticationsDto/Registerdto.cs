using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.AuthenticationsDto
{
    public class Registerdto
    {
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string UserName { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = null!;

        [DataType(DataType.PhoneNumber)]
        public string Phonenumber { get; set; } = null!;
        public int Age { get; set; }
    }
}
