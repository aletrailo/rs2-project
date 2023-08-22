using Advertising.Common.Interfaces;
using Advertising.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Advertising.Api.Dtos;
using Advertising.Api.Extensions;

namespace Advertising.Api.Controllers
{
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
        public async Task<IActionResult> AddAnAdAsync(AdInfoDto adDto)
        {
            var adInfo = adDto.ToAd();
            return Ok(await service.AddAsync(adInfo));
        }
    }
}
