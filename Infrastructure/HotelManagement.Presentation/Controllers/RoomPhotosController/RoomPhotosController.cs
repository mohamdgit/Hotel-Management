using Hotel_Management.ServiceAbstraction.ServiceManager;
using Hotel_Management.Shared.DTOs.Hotel.HotelPhotoDtos;
using Hotel_Management.Shared.DTOs.RoomDtos.RoomPhoto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Presentation.Controllers.RoomPhotosController
{
    [ApiController]
    [Route("api/{Controller}")]
    public class RoomPhotosController: ControllerBase
    {
        private readonly IServiceManager service;

        public RoomPhotosController(IServiceManager service)
        {
            this.service = service;
        }

        [RequestSizeLimit(10_485_760)] // 10 MB
        [HttpPost("Upload")]
        public async Task<ActionResult<string>> Upload([FromForm] RoomPhotoCreateDto dto)
        {
            if (dto.image == null || dto.image.Length == 0)
                return BadRequest("No file provided.");

            string folder = "RoomImages";
            var filename = await service.PhotoRoomService.AddPhoto(dto.image, folder, dto);

            string fileUrl = $"{Request.Scheme}://{Request.Host}/files/{folder}/{filename}";
            return Ok(fileUrl);
        }

        [HttpGet("GetPhotos")]
        public async Task<ActionResult<IEnumerable<RoomPhotoDto>>> GetPhotos()
        {
            var res = service.PhotoRoomService.getallphotos();
            return Ok(res);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete([FromQuery] string filename, int photoid)
        {
            string folder = "RoomImages";
            bool deleted = await service.PhotoRoomService.deletePhoto(folder, filename, photoid);

            if (deleted)
                return Ok("Deleted successfully.");
            return NotFound("File not found.");
        }
    }
}
