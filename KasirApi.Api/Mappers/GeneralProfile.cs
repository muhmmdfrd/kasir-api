using AutoMapper;
using KasirApi.Core.Models.Services;
using KasirApi.Repository.Entities;

namespace KasirApi.Api.Mappers
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<User, UserViewDto>()
                .ForMember(d => d.RoleName, conf => conf.MapFrom(e => e.Role.Code))
                .ReverseMap();
            CreateMap<UserAddDto, User>();
            CreateMap<UserUpdDto, User>();
        }
    }
}
