using AutoMapper;
using Nitaj.Application.DTOs.ToDoItems;
using Nitaj.Domain.Entities;

namespace Nitaj.Application.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoItem, ToDoItemDto>();
            CreateMap<ToDoItemDto, TodoItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
