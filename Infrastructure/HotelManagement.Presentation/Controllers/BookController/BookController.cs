using Hotel_Management.ServiceAbstraction.IBookingService;
using Hotel_Management.ServiceAbstraction.ServiceManager;
using Hotel_Management.Shared.DTOs.BookDtos;
using Hotel_Management.Shared.DTOs.BooksDtos;
using Hotel_Management.Shared.DTOs.Hotel.HotelDtos;
using Hotel_Management.Shared.ProductQueryParam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Presentation.Controllers.BookController
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IServiceManager _bookingService;

        public BookingsController(IServiceManager bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("GetBookings")]
        [Authorize(policy: "Admin")]
        public ActionResult<IEnumerable<BookDto>> GetallBookings()
        {
            var bookings = _bookingService.BookService.GetallBookings();
            return Ok(bookings);
        }

        [HttpGet("bookings/{id}")]
        [Authorize(policy: "Admin")]

        public async Task<ActionResult<BookDto>> GetBookByIdAsync(int id)
        {
            var booking = await _bookingService.BookService.GetBookByIdAsync(id);
            if (booking == null)
                return NotFound();
            return Ok(booking);
        }

        [HttpGet("specification")]
        [Authorize(policy:"Admin")]

        public ActionResult<BookDto> GetBoospecification(itemsQueryParam?param)
        { 
            var booking =  _bookingService.BookService.GetallBookingspecfications(param);
        
            return Ok(booking);
        }
        [HttpPost("AddBook")]
        [Authorize(policy: "User")]

        public async Task<ActionResult<int>> AddBookAsync([FromBody] AddBookDto dto)
        {
            var result = await _bookingService.BookService.AddBookAsync(dto);
            return Ok( result);
        }

        [HttpPut("UpdateBook/{id}")]
        [Authorize(policy: "User")]

        public async Task<ActionResult<int>> UpdateBook(int id, [FromBody] AddBookDto dto)
        {
            var result = await _bookingService.BookService.UpdateBook(id, dto);
            return Ok(result);
        }

        [HttpPost("DeleteBook/{BookingId}")]
        [Authorize(policy: "User")]

        public async Task<ActionResult> DeleteBook(int BookingId)
        {
           await _bookingService.BookService.CancelBookAsync(BookingId);
            return Ok();
        }

     
    }
}
