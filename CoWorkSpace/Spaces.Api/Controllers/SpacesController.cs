using Microsoft.AspNetCore.Mvc;
using Spaces.Api.Dtos;
using Spaces.Api.Extensions;
using Spaces.Common.Interfaces;
using System.Security.Claims;

namespace Spaces.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpacesController : ControllerBase
    {
        private readonly ISpaceService service;

        public SpacesController(ISpaceService spaceService)
        {
            this.service = spaceService;
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<SpaceDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SpaceDto>>> GetAllAsync()
        {
            return Ok((await this.service.GetAllAsync()).ToDto());
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync(CreationInfoDto creationInfoDto)
        {
            var creationInfo = creationInfoDto.ToModel();
            await service.AddAsync(creationInfo);
            return Ok();
        }

        [HttpPut("[action]")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> UpdateAsync(SpaceDto spaceDto)
        {
            var space = spaceDto.ToModel();
            return Ok(await service.UpdateAsync(space));

        }

        //getbyid
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(SpaceDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<SpaceDto>> GetByIdAsync(String Id)
        {
            return Ok(await service.GetByIdAsync(Id));

        }

        [HttpDelete("[action]")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> DeleteAsync(string Id)
        {
            return Ok(await service.DeleteAsync(Id));

        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<SpaceDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SpaceDto>>> GetAllReservedByAsync()
        {
            string username = User.FindFirst(ClaimTypes.Name).Value;

            return Ok((await service.GetAllReservedByAsync(username)).ToDto());
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<SpaceDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SpaceDto>>> GetAllOwnedByAsync()
        {
            string username = User.FindFirst(ClaimTypes.Name).Value;

            return Ok((await service.GetAllOwnedByAsync(username)).ToDto());
        }

    }
}
