using AutoMapper;
using IdentityServer.Api.DTOs;
using IdentityServer.Api.Enitities;

namespace IdentityServer.Api.Mapper
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile() {
            CreateMap<User, NewUserDto>().ReverseMap()
                .ForMember(x => x.UserName, y => y.MapFrom(user => user.UserNeme));
            CreateMap<User, UserDetails>().ReverseMap();
            
        }
    }
}
