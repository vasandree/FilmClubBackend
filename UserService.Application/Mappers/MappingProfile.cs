using AutoMapper;
using UserService.Application.Dtos.Requests;
using UserService.Application.Dtos.Responses;
using UserService.Domain.Entities;

namespace UserService.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterUserDto, ApplicationUser>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
        CreateMap<ApplicationUser, UserDto>();
        CreateMap<EditProfileDto, ApplicationUser>();
        CreateMap<UserSettings, UserSettingsDto>();
    }
}