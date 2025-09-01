using AutoMapper;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Mappings
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupDTO>().ReverseMap();
            CreateMap<CreateGroupDTO, GroupDTO>().ReverseMap();
            CreateMap<CreateGroupDTO, Group>().ReverseMap();
        }
    }
}
