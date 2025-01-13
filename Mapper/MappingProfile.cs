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
                       .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.UserPhone));
          
            CreateMap<ApplicationUser,UseViewDto>()
                      .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                      .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Email))
                      .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src =>src.createdAt))
                      .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src =>src.updatedAt))
                      .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src =>$"Images/{src.profilePicture}"))
                      .ForMember(dest => dest.UserPhone, opt => opt.MapFrom(src => src.PhoneNumber));
           
            CreateMap<CourseInputDto, Course>()
                     .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.title))
                     .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.price))
                     .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
                     .ForMember(dest => dest.category, opt => opt.MapFrom(src => src.category))
                     .ForMember(dest => dest.language, opt => opt.MapFrom(src => src.language))
                     .ForMember(dest => dest.level, opt => opt.MapFrom(src => src.level))
                     .ForMember(dest => dest.instractureId, opt => opt.MapFrom(src => src.instractureId));

            CreateMap<Course, CourseOutputDto>()
                     .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.title))
                     .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.price))
                     .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
                     .ForMember(dest => dest.category, opt => opt.MapFrom(src => src.category))
                     .ForMember(dest => dest.language, opt => opt.MapFrom(src => src.language))
                     .ForMember(dest => dest.level, opt => opt.MapFrom(src => src.level))
                     .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.updatedAt))
                     .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.updatedAt))
                     .ForMember(dest => dest.thumbnail, opt => opt.MapFrom(src => $"Images/{src.thumbnail}"))
                     .ForMember(dest => dest.instractureId, opt => opt.MapFrom(src => src.instractureId));

            CreateMap<Rating, RatingOutputDto>()
                   .ForMember(dest => dest.rating, opt => opt.MapFrom(src => src.rating))
                   .ForMember(dest => dest.studentName, opt => opt.MapFrom(src => src.Student.UserName))
                   .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
                   .ForMember(dest => dest.CourseTitile, opt => opt.MapFrom(src => src.Course.title))
                   .ForMember(dest => dest.ratingTime, opt => opt.MapFrom(src => src.ratingTime));

            CreateMap<RatingInputDto, Rating>()
                   .ForMember(dest => dest.rating, opt => opt.MapFrom(src => src.rating))
                   .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
                   .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
                   .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
                   .ForMember(dest => dest.ratingTime, opt => opt.MapFrom(src => src.ratingTime));

        }
    }
}
