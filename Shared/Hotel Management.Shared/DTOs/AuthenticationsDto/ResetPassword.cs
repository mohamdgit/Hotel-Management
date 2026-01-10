using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.AuthenticationsDto
{
    public class ResetPassworddto
    {
        public string email { get; set; }
        public string token { get; set; }
        public string newpassword { get; set; }


    }
}
