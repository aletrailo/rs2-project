﻿using Advertising.Api.Dtos;
using Advertising.Common.Models;

namespace Advertising.Api.Extensions
{
    internal static class AdExtension
    {

        internal static AdSpaceInfo ToModel(this AdInfoDto adInfoDto)
        {
            return new AdSpaceInfo
            {
                Name = adInfoDto.Name,
                Address = adInfoDto.Address,
                Description = adInfoDto.Description,
                Image = adInfoDto.Image,
                IsFree = adInfoDto.IsFree
            };

        }


    }
}
