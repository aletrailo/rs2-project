using Spaces.Api.Dtos;
using Spaces.Common.Models;

namespace Spaces.Api.Extensions
{
    internal static class CreationInfoExtensions
    {

        internal static CreationInfo ToModel(this CreationInfoDto Dto)
        {
            return new CreationInfo
            {
                Name=Dto.Name,
                Address=Dto.Address,
                Description=Dto.Description,
                Image=Dto.Image
            };


        }
        
    }
}
