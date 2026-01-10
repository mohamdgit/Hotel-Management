using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.ErrorToReturn
{
    public class ValidationError
    {
        public string feild { get; set; }
        public IEnumerable<string> error { get; set; }

    }
}
