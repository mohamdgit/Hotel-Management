using Hotel_Management.ServiceAbstraction.ServiceManager;
using Hotel_Management.Shared.DTOs.Hotel.HotelFeaturesDtos;
using Hotel_Management.Shared.DTOs.ReviewsDtos;
using Hotel_Management.Shared.ProductQueryParam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Presentation.Controllers.ReviewsController
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(policy:"User")]
    public class ReviewsController(IServiceManager service):ControllerBase
    {
        [HttpPost("AddHotelReview")]

        public async Task<ActionResult<int>> AddreviewHotel([FromBody] CreateReviewforHotelDto dto)
        {
            var res = await service.ReviewService.AddHotelReview(dto);
            return Ok(res);
        }
        [HttpPost("AddRoomReview")]

        public async Task<ActionResult<int>> AddreviewRoom([FromBody] CreateReviewforRoomDto dto)
        {
            var res = await service.ReviewService.AddRoomReview(dto);
            return Ok(res);
        }
        [HttpDelete("deleteReview")]

        public async Task<ActionResult<int>> deletereview(int Id)
        {
            var res = await service.ReviewService.DeleteReviewById( Id);
            return Ok(res);
        }
        [HttpPut("updateReview")]

        public async Task<ActionResult<int>> updateereview([FromBody] UpdateRoomReviewDto dto)
        {
            var res = await service.ReviewService.updateReviewById(dto);
            return Ok(res);
        }
        [HttpPut("updateHotelReview")]

        public async Task<ActionResult<int>> updatehotelreview([FromBody] UpdateHotelReviewDto dto)
        {
            var res = await service.ReviewService.updateHotelReviewById(dto);
            return Ok(res);
        }
        [HttpGet("GetHotelReviews")]

        public  ActionResult<IEnumerable<ReviewDto>> GetReviews(int id)
        {
            var res =  service.ReviewService.GetReviewOfHotels(id);
            return Ok(res);
        }
        [HttpGet("HotelRevspec")]

        public ActionResult<IEnumerable<ReviewDto>> GetReviewswithspec(int id,[FromBody]itemsQueryParam?param)
        {
            var res = service.ReviewService.GetReviewOfHotelswithspec(id, param);
            return Ok(res);
        }
        [HttpGet("GetroomReviews")]

        public  ActionResult<IEnumerable<ReviewDto>> GetroomReviews(int id)
        {
            var res =  service.ReviewService.GetReviewOfRooms(id);
            return Ok(res);
        }
        [HttpGet("RoomRevspec")]

        public ActionResult<IEnumerable<ReviewDto>> GetroomReviewswithspec(int id, [FromBody] itemsQueryParam? param)
        {
            var res = service.ReviewService.GetReviewOfRoomswithspec(id, param);
            return Ok(res);
        }
        [HttpGet("GetReview/{id}")]

        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {
            var res = await service.ReviewService.GetReviewById(id);
            return Ok(res);
        }
    }
}
