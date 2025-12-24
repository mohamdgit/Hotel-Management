using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.AuthenticationsDto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set ; }
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Email { get; set; } = null!;   
        public string Phonenumber { get; set; } = null!;
        public int Age { get; set; }
        public bool IsBlocked { get; set; }
        public string Token { get; set; }

    }
}
