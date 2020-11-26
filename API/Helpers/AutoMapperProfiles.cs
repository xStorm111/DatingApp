using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        //Point of automap is to map from 1 object to another
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
            .ForMember(destination => destination.PhotoUrl,
                opt => opt.MapFrom(src => src.Photos.FirstOrDefault(photo => photo.IsMain).Url)
            )
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>();
        }
    }
}