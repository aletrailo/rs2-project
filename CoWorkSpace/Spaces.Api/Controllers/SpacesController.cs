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
        public async Task InsertAsync(SpaceDto spaceDto)
        {
            spaceDto.Id=MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            await service.InsertAsync(spaceDto.ToSpace());

        }

        [HttpPut("[action]")]
        [ProducesResponseType(typeof(bool),StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync(SpaceDto spaceDto)
        {
            return Ok(await service.UpdateAsync(spaceDto.ToSpace()));

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
