using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.ServiceAbstraction.ServiceManager;
using Hotel_Management.Shared.DTOs.Hotel.HotelPhotoDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Presentation.Controllers.Hotel.HotelPhotosController
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AdminOnly")]
    [Authorize(Policy = "SuperAdminOnly")]
    public class HotelPhotoController : ControllerBase
    {
        private readonly IServiceManager service;

        public HotelPhotoController(IServiceManager service)
        {
            this.service = service;
        }

        [RequestSizeLimit(10_485_760)] // 10 MB
        [HttpPost("Upload")]
        public async Task<ActionResult<string>> Upload([FromForm] AddHotelPhotoDto dto)
        {
            if (dto.image == null || dto.image.Length == 0)
                return BadRequest("No file provided.");

            string folder = "images";
            var filename = await service.PhotoService.AddPhoto(folder, dto);

            string fileUrl = $"{filename}";
            return Ok(fileUrl);
        }

        [HttpGet("GetPhotos")]
        public async Task<ActionResult<IEnumerable<HotelPhotoDto>>> GetPhotos()
        {
            var res =  service.PhotoService.getallphotos();
            return Ok(res);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete( int photoid)
        {
            string folder = "images";
            string filename = "files";
            // نفس المجلد اللي رفعنا فيه الصور
            bool deleted = await service.PhotoService.deletePhoto(folder, filename, photoid);
            
            if (deleted)
                return Ok("Deleted successfully.");
            return NotFound("File not found.");
        }
    }

}
