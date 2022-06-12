using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser,MemberDto>() //Map AppUser to MemberDto
                .ForMember(
                    dest => dest.PhotoUrl, //Map First Photo to Main Photo
                    opt =>{
                        opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                    }
                )
                .ForMember(
                    dest => dest.Age, //Map Age to DateOfBirth
                    opt =>{
                        opt.MapFrom(src => src.DateOfBirth.CalculateAge());
                    }
                );
                
            CreateMap<Photo,PhotoDto>(); // Map Photo Entity to PhotoDto

            CreateMap<MemberUpdateDto,AppUser>(); //Map MemberUpdateDto to AppUser

            CreateMap<Post,PostDto>()
                .ForMember(
                    dest => dest.PhotoUrl, //Map First Photo to Main Photo
                    opt =>{
                        opt.MapFrom(src => src.AppUser.Photos.FirstOrDefault(p => p.IsMain).Url);
                    }
                )
                .ForMember(
                    dest => dest.Username, 
                    opt =>{
                        opt.MapFrom(src => src.AppUser.UserName);
                    }
                );

            CreateMap<Comment,CommentDto>()
                 .ForMember(
                    dest => dest.PhotoUrl, //Map First Photo to Main Photo
                    opt =>{
                        opt.MapFrom(src => src.AppUser.Photos.FirstOrDefault(p => p.IsMain).Url);
                    }
                )
                .ForMember(
                    dest => dest.UserName, 
                    opt =>{
                        opt.MapFrom(src => src.AppUser.UserName);
                    }
                );

            //Map RegisterDto to AppUser and configer username to lowercase
            CreateMap<RegisterDto,AppUser>().ForMember 
            (
                dest => dest.UserName,
                opt => opt.MapFrom(src => src.Username.ToLower())
            );

        }

        
    }
}