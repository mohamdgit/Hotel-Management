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
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.Services.HotelService
{
    public class PhotoService:IPhotoService
    {
        private readonly IUow uow;
        private readonly IMapper map;
        private readonly IHttpContextAccessor httpContext;

        public PhotoService(IUow uow, IMapper map, IHttpContextAccessor httpContext)
        {
            this.uow = uow;
            this.map = map;
            this.httpContext = httpContext;
        }

        // رفع صورة
        public async Task<string> AddPhoto(IFormFile file, string folder, AddHotelPhotoDto dto)
        {
            List<string> allowedExtensions = new List<string> { ".png", ".jpeg", ".jpg", ".gif", ".bmp", ".webp" };
            var extension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(extension))
                throw new Exception("File type not allowed");

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folder);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filename = $"{Guid.NewGuid()}-{file.FileName}";
            var filePath = Path.Combine(folderPath, filename);

            using var fstream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fstream); // ✅ Async

            var entity = map.Map<AddHotelPhotoDto, HotelPhotos>(dto);
            entity.Name = filename;

            var repo = uow.GenerateRepo<HotelPhotos, int>();
            repo.Add(entity);

            await uow.SaveChanges();

            return filename;
        }

        // جلب جميع الصور
        public IEnumerable<HotelPhotoDto> getallphotos()
        {
            var repo = uow.GenerateRepo<HotelPhotos, int>();
            var photos = repo.GetAllAsync();

            var res = map.Map<IEnumerable<HotelPhotos>, IEnumerable<HotelPhotoDto>>(photos);

            var request = httpContext.HttpContext.Request;
            foreach (var item in res)
            {
                item.Name = $"{request.Scheme}://{request.Host}/files/images/{item.Name}";
            }

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

      
    }

}
