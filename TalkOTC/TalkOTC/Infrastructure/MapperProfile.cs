using AutoMapper;
using TalkOTC.Data.Entities;
using TalkOTC.Models.DTOs;

namespace TalkOTC.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.RoomIdentifier, opt => opt.MapFrom(src => src.RoomIdentifier));
        }
    }
}
