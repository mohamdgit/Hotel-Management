using Hotel_Management.Shared.DTOs.BookDtos;
using Hotel_Management.Shared.DTOs.BooksDtos;
using Hotel_Management.Shared.DTOs.Hotel.HotelDtos;
using Hotel_Management.Shared.ProductQueryParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceAbstraction.IBookingService
{
    public interface IBookingService
    {
        public IEnumerable<BookDto> GetallBookings();
        public IEnumerable<BookDto> GetallBookingspecfications(itemsQueryParam? param);


        public Task<BookDto> GetBookByIdAsync(int Id);
        public Task CancelBookAsync(int BookingId);
        public Task<int> AddBookAsync(AddBookDto dto);
        public Task<int> UpdateBook(int id, AddBookDto dto);
    }
}
