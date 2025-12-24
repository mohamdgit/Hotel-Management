using Hotel_Management.ServiceAbstraction.ServiceManager;
using Hotel_Management.Shared.DTOs.Hotel.Features;
using Hotel_Management.Shared.DTOs.Hotel.HotelDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Presentation.Controllers.FeaturesController
{
    [ApiController]
    [Route("api/{Controller}")]
    public class FeaturesController(IServiceManager service):ControllerBase
    {
        [HttpGet("GetFeature")]

        public  ActionResult<FeaturesDto> GetFeature()
        {
            var res =  service.serviceOfFeatures.GetFeatureAsync();
            return Ok(res);
        }
        [HttpPost("AddFeature")]

        public async Task<ActionResult<HotelDto>> AddHotel([FromQuery] AddFeaturesDto dto)
        {
            var res = await service.serviceOfFeatures.AddFeatureAsync(dto);
            return Ok(res);
        }
    }
}
