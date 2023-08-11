using AutoMapper;
using IdentityServer.Api.DTOs;
using IdentityServer.Api.Enitities;

namespace IdentityServer.Api.Mapper
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile() {
            CreateMap<User, NewUserDto>().ReverseMap();
        
        }
    }
}
