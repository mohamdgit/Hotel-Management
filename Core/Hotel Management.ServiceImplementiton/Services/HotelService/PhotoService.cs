using AutoMapper;
using Hotel_Management.DOMAIN.Contracts.IUow;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.ServiceAbstraction.IserviceOfHotel;
using Hotel_Management.Shared.DTOs.Hotel.HotelPhotoDtos;
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
    public class PhotoService:IPhotoService
    {
        private readonly IUow uow;
        private readonly IMapper map;
        private readonly IHttpContextAccessor httpContextAccessor;
        private string Folder;
        public PhotoService(IUow uow, IMapper map,IHttpContextAccessor httpContextAccessor)
        {
            this.uow = uow;
            this.map = map;
            this.httpContextAccessor = httpContextAccessor;
        }

        // رفع صورة
        public async Task<string> AddPhoto(string folder, AddHotelPhotoDto dto)
        {
            var hotel =await uow.GenerateRepo<Hotel, int>().GetById(dto.HotelId);
            if(hotel is not null)
            {
                Folder = folder;
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

                var entity = map.Map<AddHotelPhotoDto, HotelPhotos>(dto);
                entity.Name = $"files/{folder}/{filename}";
                var repo = uow.GenerateRepo<HotelPhotos, int>();
                if (GetuserId() == hotel.managerId)
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

                throw new Exception("Hotel is Not Found");


            }



        }

        // جلب جميع الصور
        public IEnumerable<HotelPhotoDto> getallphotos()
        {
            var repo = uow.GenerateRepo<HotelPhotos, int>();
            var photos = repo.GetAllAsync();
           

            var res = map.Map<IEnumerable<HotelPhotos>, IEnumerable<HotelPhotoDto>>(photos);

            var request = httpContextAccessor.HttpContext.Request;
           

            return res;
        }

        // حذف صورة
        public async Task<bool> deletePhoto(string folder, string filename, int photoId)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folder, filename);

            if (File.Exists(filePath))
                File.Delete(filePath);

            var repo = uow.GenerateRepo<HotelPhotos, int>();
            var photo = await repo.GetById(photoId); // ✅ Async
            if (photo != null)
            {
                repo.Deletephoto(photo);
                await uow.SaveChanges();
                return true;
            }

            return false;
        }

        private Guid GetuserId()
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated");

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new UnauthorizedAccessException("UserId not found in token");

            return Guid.Parse(userIdClaim.Value);
        }

    }

}
