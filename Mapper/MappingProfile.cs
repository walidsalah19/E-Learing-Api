using AutoMapper;
using E_Learning.Dtos;
using E_Learning.Models;

namespace E_Learning.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, ApplicationUser>()
                       .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                       .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserEmail))
                       .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => DateTime.Now))
                       .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => DateTime.Now))
                       .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.UserPhone));

        }
    }
}
