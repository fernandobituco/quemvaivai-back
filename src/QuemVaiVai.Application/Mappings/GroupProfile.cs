using AutoMapper;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Application.Mappings
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupDTO>().ReverseMap();
            CreateMap<CreateGroupDTO, GroupDTO>().ReverseMap();
            CreateMap<CreateGroupDTO, Group>().ReverseMap();
            CreateMap<GroupDTO, GroupCardResponse>().ReverseMap();
        }
    }
}
