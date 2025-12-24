using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.AuthenticationsDto
{
    public class Emailsend
    {
        [DataType(DataType.EmailAddress)]
        public string To { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string Subject { get; set; } = null!;


    }
}
