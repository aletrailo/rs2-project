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
            Space space = spaceDto.ToModel();

            CDNImage cdnImage = await this.cdnImageService.GetAsync(space.ImageId);
            cdnImage.Blob = space.Image;

            await this.cdnImageService.UpdateAsync(cdnImage);

            return Ok(await service.UpdateAsync(space));
        }

        //getbyid
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(SpaceDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<SpaceDto>> GetByIdAsync(String Id)
        {
            Space space = await service.GetByIdAsync(Id);
            space.Image = (await this.cdnImageService.GetAsync(space.ImageId)).Blob;

            return Ok(space.ToDto());
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

            var spaceDtos = new List<SpaceDto>();

            foreach (Space space in await this.service.GetAllReservedByAsync(username))
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

            return Ok(spaceDtos);
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

            var spaceDtos = new List<SpaceDto>();

            foreach (Space space in await this.service.GetAllOwnedByAsync(username))
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

            return Ok(spaceDtos);
        }

        [HttpDelete("[action]")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> DeleteAsync(string Id)
        {
            Guid blobId = (await this.service.GetByIdAsync(Id)).ImageId;

            await this.cdnImageService.DeleteAsync(blobId);

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
