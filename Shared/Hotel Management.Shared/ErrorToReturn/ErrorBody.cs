using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.ErrorToReturn
{
    public class ErrorBody
    {
        public int statuscode { get; set; } = (int)HttpStatusCode.BadRequest;
        public string Massege { get; set; }

    }
}
