
using AutoMapper;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Mappings
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<CreateEventDTO, EventDTO>().ReverseMap();
            CreateMap<CreateEventDTO, Event>().ReverseMap();
        }
    }
}
