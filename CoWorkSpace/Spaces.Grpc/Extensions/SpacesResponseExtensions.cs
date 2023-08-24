using Spaces.Grpc.Protos;
using System.Collections.Generic;

using SpaceModel = Spaces.Common.Models.Space;
using CreationInfo = Spaces.Common.Models.CreationInfo;

namespace Spaces.Grpc.Extensions
{
    internal static class SpacesResponseExtensions
    {
        internal static Space ToGrpsModel(this SpaceModel model)
        {
            return new Space
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Description = model.Description,
                Image =  model.Image
                
            };
        }

        internal static IEnumerable<Space> ToGrpsModel(this IEnumerable<SpaceModel> models)
        {
            return models.Select(model => model.ToGrpsModel());
        }


    }
}
