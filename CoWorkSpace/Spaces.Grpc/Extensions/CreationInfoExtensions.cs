using Spaces.Common.Models;
using Spaces.Grpc.Protos;

namespace Spaces.Grpc.Extensions
{
    internal static class CreationInfoExtensions
    {
        internal static CreationInfo ToModel(this SpaceInfo spaceInfo)
        {
            return new CreationInfo
            {
                Name = spaceInfo.Name,
                Address = spaceInfo.Address,
                Description = spaceInfo.Description,
                Image = spaceInfo.Image,
                IsFree = spaceInfo.Isfree,
                PricePerHour = spaceInfo.Priceperhour,
                Owner = spaceInfo.Owner
            };
        }
    }
}
