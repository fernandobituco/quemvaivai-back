
using AutoMapper;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Mappings
{
    public class TaskListProfile : Profile
    {
        public TaskListProfile()
        {
            CreateMap<TaskList, CreateTaskListDTO>().ReverseMap();
            CreateMap<TaskList, TaskListDTO>().ReverseMap();
        }
    }
}
