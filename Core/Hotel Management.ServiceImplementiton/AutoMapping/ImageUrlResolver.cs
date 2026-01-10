using AutoMapper;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.Shared.DTOs.Hotel.HotelPhotoDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.AutoMapping
{
    public class ImageUrlResolver(IConfiguration config) : IValueResolver<HotelPhotos, HotelPhotoDto, string>
    {
        public string Resolve(HotelPhotos source, HotelPhotoDto destination, string destMember, ResolutionContext context)
        {
          return $"{config.GetSection("Urls")["baseurl"]}{source.Name}";
        }
    }
}
