using AutoMapper;
using RestaurantGroup.Identity.Application.DTOs;
using RestaurantGroup.Identity.Domain.Entities;
using System.Linq;

namespace RestaurantGroup.Identity.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map User entity to UserDto
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => 
                    src.UserRoles.Select(ur => ur.Role.Name)));
                    
            // Add any additional mappings here
        }
    }
}