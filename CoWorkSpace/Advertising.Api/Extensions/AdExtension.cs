using Advertising.Api.Dtos;
using Advertising.Common.Models;

namespace Advertising.Api.Extensions
{
    internal static class AdExtension
    {

        internal static AdSpace ToAd(this AdInfoDto adInfoDto)
        {
            return new AdSpace
            {
                Name = adInfoDto.Name,
                Address = adInfoDto.Address,
                Description = adInfoDto.Description,
                Image = adInfoDto.Image
            };

        }


    }
}
