using Hotel_Management.Shared.DTOs.ReviewsDtos;
using Hotel_Management.Shared.ProductQueryParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceAbstraction.IserviceOfHotel
{
    public interface IReviewService
    {
        public Task<int> AddHotelReview(CreateReviewforHotelDto dto);
        public Task<int> AddRoomReview(CreateReviewforRoomDto dto);

        public IEnumerable<ReviewDto> GetReviewOfHotels(int Hotelid);
        public Task<ReviewDto> GetReviewById(int Id);
        public IEnumerable<ReviewDto> GetReviewOfRooms(int Roomid);

        public Task<int> updateReviewById(UpdateRoomReviewDto dto);
        public Task<int> updateHotelReviewById(UpdateHotelReviewDto dto);

        public Task<int> DeleteReviewById(int Id);
        public IEnumerable<ReviewDto> GetReviewOfHotelswithspec(int Hotelid, itemsQueryParam? param);
        public IEnumerable<ReviewDto> GetReviewOfRoomswithspec(int Roomid, itemsQueryParam? param);


    }
}
