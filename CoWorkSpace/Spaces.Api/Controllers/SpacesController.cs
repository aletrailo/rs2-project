using Microsoft.AspNetCore.Mvc;
using Spaces.Api.Dtos;
using Spaces.Api.Extensions;
using Spaces.Common.Interfaces;
using Spaces.Common.Models;
using System.Security.Claims;

namespace Spaces.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpacesController : ControllerBase
    {
        private readonly ISpaceService service;
        private readonly ICDNImageService cdnImageService;

        public SpacesController(ISpaceService spaceService, ICDNImageService cdnImageService)
        {
            this.service = spaceService;
            this.cdnImageService = cdnImageService;
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<SpaceDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SpaceDto>>> GetAllAsync()
        {
            var spaceDtos = new List<SpaceDto>();

            foreach (Space space in await this.service.GetAllAsync())
            {
                CDNImage cdnImage = await this.cdnImageService.GetAsync(space.ImageId);
                spaceDtos.Add(new SpaceDto
                {
                    Id = space.Id,
                    Name = space.Name,
                    Address = space.Address,
                    Description = space.Description,
                    ImageId = space.ImageId,
                    Image = cdnImage.Blob,
                    IsFree = space.IsFree,
                    PricePerHour = space.PricePerHour,
                    Owner = space.Owner,
                    ReservedBy = space.ReservedBy
                });
            }

            return spaceDtos;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task AddAsync(CreationInfoDto creationInfoDto)
        {
            Guid imageId = Guid.NewGuid();

            var cdnImageCreationInfo = new CDNImageCreationInfo
            {
                BlobId = imageId,
                Blob = creationInfoDto.Image
            };
            await this.cdnImageService.AddAsync(cdnImageCreationInfo);

            var creationInfo = creationInfoDto.ToModel();
            await service.AddAsync(creationInfo, imageId);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> UpdateAsync(SpaceDto spaceDto)
        {
            //TODO: Add CDN.Grpc update
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

        
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<SpaceDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SpaceDto>>> GetAllReservedByAsync()
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;

            if (username is null)
            {
                return Unauthorized();
            }

            return Ok((await service.GetAllReservedByAsync(username)).ToDto());
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<SpaceDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SpaceDto>>> GetAllOwnedByAsync()
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;

            if (username is null)
            {
                return Unauthorized();
            }

            return Ok((await service.GetAllOwnedByAsync(username)).ToDto());
        }

        [HttpDelete("[action]")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> DeleteAsync(string Id)
        {

            return Ok(await service.DeleteAsync(Id));

        }

        [HttpDelete("drop-database")]
        public Task Dropdatabase()
        {
            //Use this only from swagger
            return Task.Run(() => this.service.DeleteAllFromDatabase());
        }
    }
}
