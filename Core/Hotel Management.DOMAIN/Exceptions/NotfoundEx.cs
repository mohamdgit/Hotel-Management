using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Exceptions
{
    public class NotfoundEx(string massege):Exception("not found!!")
    {
    }
}
