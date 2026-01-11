using AutoMapper;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Models.BookModel;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.DOMAIN.Models.PaymentModel;
using Hotel_Management.DOMAIN.Models.RoomModel;
using Hotel_Management.ServiceAbstraction.IBookingService;
using Hotel_Management.ServiceImplementiton.Specification;
using Hotel_Management.Shared.DTOs.BookDtos;
using Hotel_Management.Shared.DTOs.BooksDtos;
using Hotel_Management.Shared.DTOs.Hotel.HotelDtos;
using Hotel_Management.Shared.DTOs.ReviewsDtos;
using Hotel_Management.Shared.DTOs.RoomDtos.Room;
using Hotel_Management.Shared.ProductQueryParam;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Services.BookingService
{
    public class BookingService(IUow uow,IMapper map, IHttpContextAccessor _httpContextAccessor) : IBookingService
    {
        private Guid GetuserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated");

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new UnauthorizedAccessException("UserId not found in token");

            return Guid.Parse(userIdClaim.Value);
        }
        public async Task<int> AddBookAsync(AddBookDto dto)
        {
            using var trans = await uow.BeginTransactionAsync(IsolationLevel.Serializable);
            try
            {
                var bookRepo = uow.GenerateRepo<Book, int>();
                var roomRepo = uow.GenerateRepo<Room, int>();

                if (dto.roomids.Count > 4)
                    throw new Exception("Not allowed to book more than 4 rooms");
                var rooms = new List<Room>();

                foreach (var roomId in dto.roomids)
                {
                    var room = await roomRepo.GetById(roomId);
                    if (room is null)
                        throw new Exception($"Room {roomId} not found");

                    if (room.RoomState != State.Avaliable)
                        throw new Exception($"Room {roomId} not available");

                    room.RoomState = State.pinding; 
                    rooms.Add(room);
                }

                var booking = map.Map<Book>(dto);
                booking.Bookstate = BookState.Pending;
                booking.Createdat = DateTime.Now;
                booking.RoomBooked = rooms;

                bookRepo.Add(booking);
                var res = await uow.SaveChanges();
                await trans.CommitAsync();

                return res;
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        protected async Task ConfirmBookAsync(int BookingId)
        {
            using var trans = await uow.BeginTransactionAsync(IsolationLevel.Serializable);

            try
            {
                var bookrepo = uow.GenerateRepo<Book, int>();
                itemsQueryParam? param = new itemsQueryParam();
                param.BookId = BookingId;
                var spec = new BookigSpecification(param);
                var book =  bookrepo.GetAllSpecificationAsync(spec).FirstOrDefault();
                if (book is null)
                    throw new Exception("book is not found");

                if (book.Bookstate != BookState.Pending)
                    throw new Exception("book is not Avaliable Already");

                var time = (DateTime.Now - book.Createdat).Hours;

                if (book.BookPayment.PaymentState == PaymentState.done && time < 12)
                {
                    book.Bookstate = BookState.Confirmed;

                    foreach (var item in book.RoomBooked)
                    {
                      
                        if (item is null)
                            throw new Exception($"Room {item} is not found");

                        if (item.RoomState != State.pinding)
                            throw new Exception($"Room {item} is not Avaliable Already");

                        item.RoomState = State.Booked;
                        
                    }
                }
                else
                {
                    await CancelBookAsync(BookingId); 
                    return;
                }

                bookrepo.Update(book);
                await uow.SaveChanges();
                await trans.CommitAsync();
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task CancelBookAsync(int BookingId)
        {
            using var trans = await uow.BeginTransactionAsync(IsolationLevel.Serializable);

            try
            {
                var bookrepo = uow.GenerateRepo<Book, int>();
                itemsQueryParam? param = new itemsQueryParam();
                param.BookId = BookingId;
                var spec = new BookigSpecification(param);
                var book = bookrepo.GetAllSpecificationAsync(spec).FirstOrDefault();
                if (book is null)
                    throw new Exception("book is not found");

                if (book.Bookstate != BookState.Pending)
                    throw new Exception("book is not Avaliable Already");
                if(book.UserId!= GetuserId())
                    throw new Exception("Not Allowed To You");

                book.Bookstate = BookState.Cancelled;

                foreach (var item in book.RoomBooked)
                {
                    if (item is null)
                        throw new Exception($"Room {item} is not found");

                    if (item.RoomState == State.Booked)
                        throw new Exception($"Room {item} is already booked");

                    item.RoomState = State.Avaliable;
                   
                }

                book.IsDeleted = true;
                bookrepo.Update(book);
                await uow.SaveChanges();
                await trans.CommitAsync();
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }


        public IEnumerable<BookDto> GetallBookings()
        {
            var bookRepo = uow.GenerateRepo<Book, int>();
            var bookings = bookRepo.GetAllAsync();
            var res= map.Map<IEnumerable<BookDto>>(bookings);
            foreach (var item in res)
            {
                foreach (var b in bookings)
                {
                    item.Bookstate = (BookStateDto)b.Bookstate;

                }
            }
            
            return res;
        }

        public IEnumerable<BookDto> GetallBookingspecfications(itemsQueryParam? param)
        {
            var reviewRepo = uow.GenerateRepo<Book, int>();
            var spec = new BookigSpecification(param);
            var books = reviewRepo.GetAllSpecificationAsync(spec);
        
            return map.Map<IEnumerable<Book>, IEnumerable<BookDto>>(books).ToList();
        }

        public async Task<BookDto> GetBookByIdAsync(int Id)
        {
            var bookRepo = uow.GenerateRepo<Book, int>();
            var booking = await bookRepo.GetById(Id);

            if (booking == null || booking.IsDeleted)
                throw new Exception("Booking not found");

            var res= map.Map<BookDto>(booking);
            res.Bookstate = (BookStateDto)booking.Bookstate;
            return res;
        }

        public async Task<int> UpdateBook(int id, AddBookDto dto)
        {
            using var trans = await uow.BeginTransactionAsync(IsolationLevel.Serializable);

            try
            {
                var bookrepo = uow.GenerateRepo<Book, int>();
                itemsQueryParam? param = new itemsQueryParam();
                param.BookId = id;
                var spec = new BookigSpecification(param);
                var book = bookrepo.GetAllSpecificationAsync(spec).FirstOrDefault();
                if (book == null)
                    throw new Exception($"Booking with ID {id} not found");

                if (dto.roomids.Count > 4)
                    throw new Exception("Not allowed to book more than 4 rooms");

                foreach (var item in book.RoomBooked)
                {
                    if (item is null)
                        throw new Exception($"Room {item} is not found");

                    if (item.RoomState == State.Booked)
                        throw new Exception($"Room {item} is already booked");

                    item.RoomState = State.pinding;

                }

                book.Fromdate = dto.Fromdate;
                book.Todate = dto.Todate;
                book.Bookstate = BookState.Pending;
                book.Createdat = DateTime.UtcNow;

                foreach (var room in book.RoomBooked)
                {
                    book.RoomBooked.Add(room);


                }
                book.Id = id;
                bookrepo.Update(book);

                var res = await uow.SaveChanges();
                await trans.CommitAsync();

                return res;
            }
            catch
            {
                await trans.RollbackAsync();
                throw;
            }
        }
       

    }
}
