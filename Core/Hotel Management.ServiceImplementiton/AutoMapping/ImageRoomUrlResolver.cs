using AutoMapper;
using Hotel_Management.DOMAIN.Models.RoomModel;
using Hotel_Management.Shared.DTOs.RoomDtos.RoomPhoto;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.ServiceImplementiton.AutoMapping
{
    public class ImageRoomUrlResolver(IConfiguration config) : IValueResolver<RoomPhoto, RoomPhotoDto, string>
    {
        public string Resolve(RoomPhoto source, RoomPhotoDto destination, string destMember, ResolutionContext context)
        {
            return $"{config.GetSection("Urls")["baseurl"]}{source.Url}";
        }
    }
}
