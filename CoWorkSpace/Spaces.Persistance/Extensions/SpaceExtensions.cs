﻿using Spaces.Common.Models;
using Spaces.Persistance.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Spaces.Persistance.Extensions
{
    internal static class SpaceExtensions
    {
        public static Space ToModel(this SpaceEntity entity)
        {
            return new Space
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                Description= entity.Description,
                ImageId = entity.ImageId,
                IsFree = entity.IsFree,
                PricePerHour = entity.PricePerHour,
                Owner = entity.Owner,
                ReservedBy = entity.ReservedBy
            };
        }

        public static IEnumerable<Space> ToModel(this IEnumerable<SpaceEntity> entities)
        {
            return entities.Select(entity => entity.ToModel());
        }

        public static SpaceEntity ToEntity(this Space space)
        {
            return new SpaceEntity
            {
                Id = space.Id,
                Name = space.Name,
                Address = space.Address,
                Description = space.Description,
                ImageId = space.ImageId,
                IsFree = space.IsFree,
                PricePerHour = space.PricePerHour,
                Owner = space.Owner,
                ReservedBy = space.ReservedBy
            };
        }

    }
}
