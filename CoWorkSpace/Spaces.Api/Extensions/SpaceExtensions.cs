﻿using Spaces.Api.Dtos;
using Spaces.Common.Models;

namespace Spaces.Api.Extensions
{
    internal static class SpaceExtensions
    {
        internal static SpaceDto ToDto(this Space model)
        {
            return new SpaceDto
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Description = model.Description,
                ImageId = model.ImageId,
                Image = model.Image,
                IsFree = model.IsFree,
                PricePerHour = model.PricePerHour,
                Owner = model.Owner,
                ReservedBy = model.ReservedBy

            };
        }

        internal static IEnumerable<SpaceDto> ToDto(this IEnumerable<Space> models)
        {
            return models.Select(model => model.ToDto());
        }

        internal static Space ToModel(this SpaceDto spacedto)
        {
            return new Space
            {
                Id = spacedto.Id,
                Name = spacedto.Name,
                Address = spacedto.Address,
                Description = spacedto.Description,
                ImageId = spacedto.ImageId,
                Image = spacedto.Image,
                IsFree = spacedto.IsFree,
                PricePerHour = spacedto.PricePerHour,
                Owner = spacedto.Owner,
                ReservedBy = spacedto.ReservedBy
            };
        }
    }
}
