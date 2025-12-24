using AutoMapper;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Models.BookModel;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.DOMAIN.Models.ReviewsModel;
using Hotel_Management.ServiceAbstraction.IserviceOfHotel;
using Hotel_Management.Shared.DTOs.ReviewsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Services.ReviewService
{
    public class ReviewService(IUow uow,IMapper mapp) : IReviewService
    {
       
        public async Task<int> AddHotelReview(CreateReviewforHotelDto dto)
        {

            var reviewRepo = uow.GenerateRepo<Review, int>();
            var bookingRepo = uow.GenerateRepo<Book, int>();

            var booking = await bookingRepo.GetById(dto.BookId);


            if (booking is null)
                throw new InvalidOperationException("Booking not found.");

            if (booking.UserId != dto.UserId ||
                booking.HotelId != dto.HotelId

              )
            {
                throw new UnauthorizedAccessException("you cannot add review.");
            }
            var review = mapp.Map<CreateReviewforHotelDto, Review>(dto);
            await reviewRepo.Add(review);
            return await uow.SaveChanges();
        }

        public async Task<int> AddRoomReview(CreateReviewforRoomDto dto)
        {

            var reviewRepo = uow.GenerateRepo<Review, int>();
            var bookingRepo = uow.GenerateRepo<Book, int>();

            var booking = await bookingRepo.GetById(dto.BookId);


            if (booking is null)
                throw new InvalidOperationException("Booking not found.");

            if (booking.UserId != dto.UserId ||
                booking.RoomId != dto.RoomId||
                booking.Id!=dto.BookId
              )
            {
                throw new UnauthorizedAccessException("you cannot add review.");
            }
            var review = mapp.Map<CreateReviewforRoomDto, Review>(dto);
            await reviewRepo.Add(review);
            return await uow.SaveChanges();
        }

        public async Task<int> DeleteReviewById(int Id)
        {
            var reviewRepo = uow.GenerateRepo<Review, int>();
            var bookingRepo = uow.GenerateRepo<Book, int>();

            var review = await reviewRepo.GetById(Id)
                ?? throw new InvalidOperationException("Review not found.");

            var booking = await bookingRepo.GetById(review.Bookid)
                ?? throw new InvalidOperationException("Booking not found.");

            if (review.UserId != booking.UserId ||
               
                review.HotelId != booking.HotelId)
            {
                throw new UnauthorizedAccessException("not allowed to delete this review.");
            }



            reviewRepo.Delete(Id);

            return await uow.SaveChanges();
        }

        public async Task<ReviewDto> GetReviewById(int Id)
        {
            var reviewRepo = uow.GenerateRepo<Review, int>();

            var review = await reviewRepo.GetById(Id);
            if (review is null)
                throw new InvalidOperationException("Review not found.");

           


            return mapp.Map<Review, ReviewDto>(review); ;
        }

        public IEnumerable<ReviewDto> GetReviewOfHotels(int Hotelid)
        {
            var reviewRepo = uow.GenerateRepo<Review, int>();

            var review =  reviewRepo.GetAllAsync() ;

            var rev = review.Where(p => p.HotelId == Hotelid);



            return mapp.Map<IEnumerable< Review>,IEnumerable< ReviewDto>>(rev).ToList() ;
        }

        public  IEnumerable<ReviewDto> GetReviewOfRooms(int Roomid)
        {
            var reviewRepo = uow.GenerateRepo<Review, int>();
            var review =  reviewRepo.GetAllAsync();

            var rev = review.Where(p => p.RoomID == Roomid);



            return mapp.Map<IEnumerable<Review>, IEnumerable<ReviewDto>>(rev).ToList() ;
        }

        public async Task<int> updateReviewById(UpdateRoomReviewDto dto)
        {

            var reviewRepo = uow.GenerateRepo<Review, int>();

            var review = await reviewRepo.GetById(dto.Id);


            if (review is null)
                throw new InvalidOperationException("review not found.");

            if (review.UserId != dto.UserId)
            {
                throw new UnauthorizedAccessException("You Cant update this review");
            }
            var z = mapp.Map<UpdateRoomReviewDto, Review>(dto);
             reviewRepo.Update(z);
            return await uow.SaveChanges();
        }
    }
}
