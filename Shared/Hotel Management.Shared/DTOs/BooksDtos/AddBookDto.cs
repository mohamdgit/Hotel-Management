using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Shared.DTOs.BooksDtos
{
    public class AddBookDto
    {
        public DateTime Createdat { get; set; } = DateTime.Now;

        public int NumOfDays { get; set; }
        public DateTime Fromdate { get; set; }
        public DateTime Todate { get; set; }
        public Guid UserId { get; set; }
        public List<int> roomids { get; set; } = new List<int>();
        public int HotelId { get; set; }

    }
}
