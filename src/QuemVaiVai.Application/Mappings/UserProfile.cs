using AutoMapper;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Responses;

namespace QuemVaiVai.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<CreatedUserResponse, UserDTO>().ReverseMap();
            CreateMap<User, CreateUserDTO>().ReverseMap();
        }
    }
}
