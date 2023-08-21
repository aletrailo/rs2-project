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
        public async Task InsertAsync(CreationInfoDto Dto)
        {
            var creationInfo = Dto.ToCreationInfo();
            await service.InsertAsync(creationInfo);

        }

        [HttpPut("[action]")]
        [ProducesResponseType(typeof(bool),StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync(CreationInfoDto Dto)
        {
            var creationInfo = Dto.ToCreationInfo();
            return Ok(await service.UpdateAsync(creationInfo));

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
            return Ok(await service.DeleteAsync(spaceDto.ToSpace().Id));

        }

    }
}
