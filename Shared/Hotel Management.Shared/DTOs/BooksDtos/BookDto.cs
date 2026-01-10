using Hotel_Management.Shared.ProductQueryParam;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.BookDtos
{
    public class BookDto
    {
        public BookStateDto Bookstate { get; set; }
        public int NumOfDays { get; set; }
        public DateTime Createdat { get; set; }

        public DateTime Fromdate { get; set; }
        public DateTime Todate { get; set; }
        public Guid UserId { get; set; }
        public List<int> roomids { get; set; } = new List<int>();
        public int HotelId { get; set; }

        public bool IsCanceled { get; set; }
        public bool IsDeleted { get; set; }
    }
}
