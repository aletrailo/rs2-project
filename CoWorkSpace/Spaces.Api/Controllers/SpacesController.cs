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
    }
}
