using Hotel_Management.DOMAIN.Models.BookModel;
using Hotel_Management.Shared.ProductQueryParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Specification
{
    public class BookigSpecification:BaseSpecification<Book,int>
    {
        public BookigSpecification(itemsQueryParam? item):base(p=>
             item != null && item.BookState.HasValue
            ? p.Bookstate == (BookState)item.BookState.Value
            : true && item.BookId.HasValue
            ? p.Id == item.BookId.Value
            : true
        )
        {
            Addincludesfunc(p => p.BookPayment);
            Addincludesfunc(p => p.User);
            Addincludesfunc(p => p.Hotel);
            Addincludesfunc(p => p.ReviewBook);
            Addincludesfunc(p => p.RoomBooked);
            
        }
    }
}
