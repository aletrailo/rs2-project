using Advertising.Common.Interfaces;
using Advertising.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Advertising.Api.Dtos;
using Advertising.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Advertising.Api.Controllers
{
    [Authorize(Roles ="User")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisingController : ControllerBase
    {
        private readonly IAdvertisingService service;

        public AdvertisingController(IAdvertisingService advertisingService)
        {
            this.service = advertisingService;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<AdSpace>> GetAllAsync()
        {
            return await this.service.GetAllAsync();
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAnAdAsync(AdInfoDto adInfoDto)
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;

            AdSpaceInfo adSpaceInfo = adInfoDto.ToModel(username);
            return Ok(await service.AddAsync(adSpaceInfo));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> BookASpaceAsync(string spaceId)
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;
            
            ReservationInfo reservationInfo=new ReservationInfo(username, spaceId);
            return Ok(await service.BookASpaceAsync(reservationInfo));

        }
    }
}
