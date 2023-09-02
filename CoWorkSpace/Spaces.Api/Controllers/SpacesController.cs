using Microsoft.AspNetCore.Mvc;
using Spaces.Api.Dtos;
using Spaces.Api.Extensions;
using Spaces.Common.Interfaces;
using Spaces.Common.Models;

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
        public async Task<IEnumerable<SpaceDto>> GetAllAsync()
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
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(bool),StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync(SpaceDto spaceDto)
        {
            //TODO: Add CDN.Grpc update
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
            //TODO: Add CDN.Grpc update
            return Ok(await service.DeleteAsync(spaceDto.ToModel().Id));
        }

    }
}
