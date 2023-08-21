using Spaces.Persistance.Entities;
using Spaces.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spaces.Persistance.Extensions
{
    internal static class CreationInfoExtensions
    {

        public static SpaceEntity ToSpaceEntity(this CreationInfo creationInfo)
        {
            return new SpaceEntity
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                Name = creationInfo.Name,
                Address = creationInfo.Address,
                Description = creationInfo.Description,
                Image = creationInfo.Image
            };
        }
    }
}
