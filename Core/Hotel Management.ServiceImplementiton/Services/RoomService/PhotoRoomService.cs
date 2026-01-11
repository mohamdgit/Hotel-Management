using AutoMapper;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.DOMAIN.Models.RoomModel;
using Hotel_Management.ServiceAbstraction.IserviceOfHotel;
using Hotel_Management.ServiceImplementiton.Specification;
using Hotel_Management.Shared.DTOs.Hotel.HotelPhotoDtos;
using Hotel_Management.Shared.DTOs.RoomDtos.RoomPhoto;
using Hotel_Management.Shared.ProductQueryParam;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Services.HotelService
{
    public class PhotoRoomService:IPhotoRoomService
    {
        private readonly IUow uow;
        private readonly IMapper map;
        private readonly IHttpContextAccessor httpContext;

        public PhotoRoomService(IUow uow, IMapper map, IHttpContextAccessor httpContext)
        {
            this.uow = uow;
            this.map = map;
            this.httpContext = httpContext;
        }
        private Guid GetuserId()
        {
            var user = httpContext.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated");

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new UnauthorizedAccessException("UserId not found in token");

            return Guid.Parse(userIdClaim.Value);
        }
        // رفع صورة
        public async Task<string> AddPhoto( string folder, RoomPhotoCreateDto dto)
        {
            itemsQueryParam? item = new itemsQueryParam();
            item.RoomId = dto.RoomId;
            var spec = new RoomSpecification(item);
            var room =  uow.GenerateRepo<Room, int>().GetAllSpecificationAsync(spec).FirstOrDefault();
            if(room is not null)
            {
                List<string> allowedExtensions = new List<string> { ".png", ".jpeg", ".jpg", ".gif", ".bmp", ".webp" };
                var extension = Path.GetExtension(dto.image.FileName);
                if (!allowedExtensions.Contains(extension))
                    throw new Exception("File type not allowed");

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folder);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filename = $"{Guid.NewGuid()}-{dto.image.FileName}";
                var filePath = Path.Combine(folderPath, filename);

                using var fstream = new FileStream(filePath, FileMode.Create);
                await dto.image.CopyToAsync(fstream);

                var entity = map.Map<RoomPhotoCreateDto, RoomPhoto>(dto);
                entity.Url = $"files/{folder}/{filename}";

                var repo = uow.GenerateRepo<RoomPhoto, int>();
                if (GetuserId() == room.Hotel.managerId)
                {
                    repo.Add(entity);

                }
                else
                {
                    throw new Exception("Not Allowed To You");
                }

                await uow.SaveChanges();

                return filename;
            }
            else
            {
                await uow.SaveChanges();

                throw new Exception("Not found");

            }

        }

       
        public IEnumerable<RoomPhotoDto> getallphotos()
        {
            var repo = uow.GenerateRepo<RoomPhoto, int>();
            var photos = repo.GetAllAsync().ToList();

            var res = map.Map<IEnumerable<RoomPhoto>, IEnumerable<RoomPhotoDto>>(photos);

            var request = httpContext.HttpContext.Request;
            

            return res;

        }

        // حذف صورة
        public async Task<bool> deletePhoto(string folder, string filename, int photoId)
        {

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folder, filename);

            if (File.Exists(filePath))
                File.Delete(filePath);

            var repo = uow.GenerateRepo<RoomPhoto, int>();
            var photo = await repo.GetById(photoId); 
            if (photo != null)
            {
                repo.Deletephoto(photo);
                await uow.SaveChanges();
                return true;
            }

            return false;
        }

      
    }

}
