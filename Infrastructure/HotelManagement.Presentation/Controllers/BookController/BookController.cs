using Hotel_Management.ServiceAbstraction.IBookingService;
using Hotel_Management.ServiceAbstraction.ServiceManager;
using Hotel_Management.Shared.DTOs.BookDtos;
using Hotel_Management.Shared.DTOs.BooksDtos;
using Hotel_Management.Shared.DTOs.Hotel.HotelDtos;
using Hotel_Management.Shared.ProductQueryParam;
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
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET: api/bookings
        [HttpGet("GetBookings")]
        public ActionResult<IEnumerable<BookDto>> GetallBookings()
        {
            var bookings = _bookingService.GetallBookings();
            return Ok(bookings);
        }

        // GET: api/bookings/{id}
        [HttpGet("bookings/{id}")]
        public async Task<ActionResult<BookDto>> GetBookByIdAsync(int id)
        {
            var booking = await _bookingService.GetBookByIdAsync(id);
            if (booking == null)
                return NotFound();
            return Ok(booking);
        }
        // GET: api/bookings/{id}

        [HttpGet("specification/{id}")]
        public  ActionResult<BookDto> GetBoospecification(itemsQueryParam?param)
        { 
            var booking =  _bookingService.GetallBookingspecfications(param);
        
            return Ok(booking);
        }
        // POST: api/bookings
        [HttpPost("AddBook")]
        public async Task<ActionResult<int>> AddBookAsync([FromBody] AddBookDto dto)
        {
            var result = await _bookingService.AddBookAsync(dto);
            return Ok( result);
        }

        // PUT: api/bookings/{id}
        [HttpPut("AddBook/{id}")]
        public async Task<ActionResult<int>> UpdateBook(int id, [FromBody] AddBookDto dto)
        {
            var result = await _bookingService.UpdateBook(id, dto);
            return Ok(result);
        }

        // SoftDELETE: api/bookings/{id}
        [HttpPost("DeleteBook/{BookingId}")]
        public async Task<ActionResult> DeleteBook(int BookingId)
        {
           await _bookingService.CancelBookAsync(BookingId);
            return Ok();
        }

     
    }
}
