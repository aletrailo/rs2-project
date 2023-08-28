using Spaces.Api.Dtos;
using Spaces.Common.Models;

namespace Spaces.Api.Extensions
{
    internal static class CreationInfoExtensions
    {

        internal static CreationInfo ToModel(this CreationInfoDto creationInfoDto)
        {
            return new CreationInfo
            {
                Name=creationInfoDto.Name,
                Address=creationInfoDto.Address,
                Description=creationInfoDto.Description,
                Image=creationInfoDto.Image,
                IsFree=creationInfoDto.IsFree,
                PricePerHour=creationInfoDto.PricePerHour,
                Owner=creationInfoDto.Owner,

            };


        }
        
    }
}
