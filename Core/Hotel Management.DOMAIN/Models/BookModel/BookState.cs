using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.BookModel
{
    public enum BookState
    {
        Pending = 1,       
        Confirmed = 2,     
        CheckedIn = 3,     
        CheckedOut = 4,    
        Refused = 5,       
        Cancelled = 6,     
        NoShow = 7         
    }
}
