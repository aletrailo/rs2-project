using Advertising.Common.Interfaces;
using Advertising.Common.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
