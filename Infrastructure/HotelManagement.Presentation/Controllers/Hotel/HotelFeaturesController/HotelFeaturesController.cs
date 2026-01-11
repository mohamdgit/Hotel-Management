using Hotel_Management.ServiceAbstraction.ServiceManager;
using Hotel_Management.Shared.DTOs.Hotel.HotelDtos;
using Hotel_Management.Shared.DTOs.Hotel.HotelFeaturesDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Presentation.Controllers.Hotel.HotelFeaturesController
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(Policy = "AdminOrSuperAdmin")]
    
    public class HotelFeaturesController(IServiceManager service):ControllerBase
    {
            [HttpPost("AddHotelFeature")]
            public async Task<ActionResult<int>> AddHotel([FromBody] AddHotelFeaturesDto dto)
            {
                var res = await service.serviceOfHotelFeature.Add(dto);
                return Ok(res);
            }

            [HttpGet("GetHotelFeature/{Id}")]

            public async Task<ActionResult< IEnumerable<HotelFeaturesDto>>> GetHotelFeature(int Id)
            {
                var res = await service.serviceOfHotelFeature.GetHotelFeaturesAsync(Id);
                return Ok(res);
            }
            [HttpDelete("deleteHotelFeature/{HotelId}/{FeatureId}")]

            public async Task<ActionResult<IEnumerable<HotelFeaturesDto>>> Delete(int HotelId, int FeatureId )
            {
                var res = await service.serviceOfHotelFeature.RemoveFeatureFromHotelAsync(HotelId, FeatureId);
                return Ok(res);
            }
    }
}
