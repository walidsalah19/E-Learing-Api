using AutoMapper;
using E_Learning.Dtos;
using E_Learning.Models;

namespace E_Learning.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDto, ApplicationUser>()
                       .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                       .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserEmail))
                       .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => DateTime.Now))
                       .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => DateTime.Now))
                       .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.UserPhone));
          
            CreateMap<ApplicationUser,UseViewDto>()
                      .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                      .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Email))
                      .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src =>src.createdAt))
                      .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src =>src.updatedAt))
                      .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src =>$"Images/{src.profilePicture}"))
                      .ForMember(dest => dest.UserPhone, opt => opt.MapFrom(src => src.PhoneNumber));

        }
    }
}
