using Advertising.Common.Interfaces;
using Advertising.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Advertising.Api.Dtos;
using Advertising.Api.Extensions;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace Advertising.Api.Controllers
{
    [Authorize(Roles = "Admin,User")]
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
        [ProducesResponseType(typeof(IEnumerable<AdSpace>), StatusCodes.Status200OK)]
        public async Task<ActionResult<AdSpace>> GetAllAsync()
        {
            return Ok(await this.service.GetAllAsync());
        }


        [HttpPost("[action]")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> AddAnAdAsync([Required]  AdInfoDto adInfoDto)
        {

            var username = User.FindFirst(ClaimTypes.Name).Value;

            if (username is null) {
                return Unauthorized();
            }
            
            AdSpaceInfo adSpaceInfo = adInfoDto.ToModel(username);
            return Ok(await service.AddAsync(adSpaceInfo));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> BookASpaceAsync(string spaceId)
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;

            if (username is null) {
                return Unauthorized();
            }

            UsernameSpaceIdInfo usernameSpaceIdInfo = new UsernameSpaceIdInfo(username, spaceId);
            return Ok(await service.BookASpaceAsync(usernameSpaceIdInfo));

        }

        [HttpDelete("[action]")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> DeleteAdAsync(string spaceId)
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;

            if (username is null)
            {
                return Unauthorized();
            }

            UsernameSpaceIdInfo usernameSpaceIdInfo = new UsernameSpaceIdInfo(username, spaceId);
            return Ok(await service.DeleteAdAsync(usernameSpaceIdInfo));


        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> EndUpUsingSpaceAsync(string spaceId)
        {
            return Ok(await service.EndUpUsingSpaceAsync(spaceId));
        }

    }
}
