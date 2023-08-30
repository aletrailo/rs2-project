using CDN.Api.Dtos;
using CDN.Api.Extensions;
using CDN.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CDN.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService service;

        public ImageController(IImageService imageService)
        {
            this.service = imageService;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<ImageDto>> GetAllAsync()
        {
            return (await this.service.GetAllAsync()).ToDto();
        }

        [HttpGet("{blobId}")]
        public async Task<ImageDto> GetAsync(Guid blobId)
        {
            return (await this.service.GetAsync(blobId)).ToDto();
        }

        [HttpPost]
        public async Task<ImageDto> AddAsync(ImageCreationInfoDto imageCreationInfoDto)
        {
            return (await this.service.AddAsync(imageCreationInfoDto.ToModel())).ToDto();
        }
    }
}
