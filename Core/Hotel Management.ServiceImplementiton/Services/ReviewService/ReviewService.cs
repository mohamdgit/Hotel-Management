using AutoMapper;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Models.BookModel;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.DOMAIN.Models.ReviewsModel;
using Hotel_Management.ServiceAbstraction.IserviceOfHotel;
using Hotel_Management.ServiceImplementiton.Specification;
using Hotel_Management.Shared.DTOs.Hotel.HotelDtos;
using Hotel_Management.Shared.DTOs.ReviewsDtos;
using Hotel_Management.Shared.ProductQueryParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Services.ReviewService
{
    public class ReviewService(IUow uow,IMapper mapp) : IReviewService
    {
       //while making review use bookid 2004 ,hotelid3 ,roomid 1005 temporary
       ///if you want to add you must complete booking and it relatrd with payment
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
            if(booking.Bookstate!=BookState.Confirmed)
            {
                throw new UnauthorizedAccessException("you cannot add review before complete booking");
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
                booking.Id!=dto.BookId
              )
            {
                throw new UnauthorizedAccessException("you cannot add review.");
            }
            if (booking.Bookstate != BookState.Confirmed )
            {
                throw new UnauthorizedAccessException("you cannot add review before complete booking");
            }
            var review = mapp.Map<CreateReviewforRoomDto, Review>(dto);
            await reviewRepo.Add(review);
            return await uow.SaveChanges();
        }

        public async Task<int> DeleteReviewById(int Id)
        {
            var reviewRepo = uow.GenerateRepo<Review, int>();
            itemsQueryParam? item = new itemsQueryParam();
            item.ReviewId = Id;
            var spec = new ReviewSpecification(item);
            var review =  reviewRepo.GetAllSpecificationAsync(spec).FirstOrDefault()
                ?? throw new InvalidOperationException("Review not found.");

           if(review.BookReview is null)
                 throw new InvalidOperationException("Booking not found.");

            if (review.UserId != review.BookReview.UserId ||
               
                review.HotelId != review.BookReview.HotelId)
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
        public IEnumerable<ReviewDto> GetReviewOfHotelswithspec(int Hotelid, itemsQueryParam? param)
        {
            var reviewRepo = uow.GenerateRepo<Review, int>();
            var spec = new ReviewSpecification(param);

            var review = reviewRepo.GetAllSpecificationAsync(spec);

            var rev = review.Where(p => p.HotelId == Hotelid);



            return mapp.Map<IEnumerable<Review>, IEnumerable<ReviewDto>>(rev).ToList();
        }

        public  IEnumerable<ReviewDto> GetReviewOfRooms(int Roomid)
        {
            var reviewRepo = uow.GenerateRepo<Review, int>();
            var review =  reviewRepo.GetAllAsync();

            var rev = review.Where(p => p.RoomID == Roomid);



            return mapp.Map<IEnumerable<Review>, IEnumerable<ReviewDto>>(rev).ToList() ;
        }
        public IEnumerable<ReviewDto> GetReviewOfRoomswithspec(int Roomid, itemsQueryParam? param)
        {
            var reviewRepo = uow.GenerateRepo<Review, int>();
            var spec = new ReviewSpecification(param);
            var query = reviewRepo.GetAllSpecificationAsync(spec).ToList();

            var review = reviewRepo.GetAllAsync();

            var rev = review.Where(p => p.RoomID == Roomid);



            return mapp.Map<IEnumerable<Review>, IEnumerable<ReviewDto>>(rev).ToList();
        }

        public async Task<int> updateHotelReviewById(UpdateHotelReviewDto dto)
        {
            var reviewRepo = uow.GenerateRepo<Review, int>();

            var review = await reviewRepo.GetById(dto.Id);


            if (review is null)
                throw new InvalidOperationException("review not found.");

            if (review.UserId != dto.UserId)
            {
                throw new UnauthorizedAccessException("You Cant update this review");
            }
            mapp.Map(dto,review);
            reviewRepo.Update(review);
            return await uow.SaveChanges();
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
            mapp.Map(dto, review);
            reviewRepo.Update(review);
            return await uow.SaveChanges();
        }
        
    }
}
