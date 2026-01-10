using Hotel_Management.Shared.DTOs.AuthenticationsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Services.ServiceOfAuthentication
{
    public static class Helper
    {
        public static void sendemail(Emailsend email)
        {
            var client = new SmtpClient("smtp.gmail.com",587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("mohamedaramadan130@gmail.com", "uaamhpbaufzlufyh");
            client.Send("mo40580944@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
