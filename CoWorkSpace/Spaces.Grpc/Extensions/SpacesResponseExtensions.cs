using Spaces.Grpc.Protos;

using SpaceModel = Spaces.Common.Models.Space;

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
                Address = model.Address
            };
        }

        internal static IEnumerable<Space> ToGrpsModel(this IEnumerable<SpaceModel> models)
        {
            return models.Select(model => model.ToGrpsModel());
        }
    }
}
