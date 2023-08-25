using Microsoft.AspNetCore.Mvc;
using Spaces.Api.Dtos;
using Spaces.Api.Extensions;
using Spaces.Common.Interfaces;

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
        public async Task<IEnumerable<SpaceDto>> GetAllAsync()
        {
            return (await this.service.GetAllAsync()).ToDto();
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task AddAsync(CreationInfoDto creationInfoDto)
        {
            var creationInfo = creationInfoDto.ToModel();
            await service.AddAsync(creationInfo);

        }

        [HttpPut("[action]")]
        [ProducesResponseType(typeof(bool),StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync(SpaceDto spaceDto)
        {
            var space = spaceDto.ToModel();
            return Ok(await service.UpdateAsync(space));

        }

        //getbyid
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(SpaceDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(String Id)
        {
            return Ok(await service.GetByIdAsync(Id));

        }
        
        [HttpDelete("[action]")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(SpaceDto spaceDto)
        {
            return Ok(await service.DeleteAsync(spaceDto.ToModel().Id));

        }

    }
}
