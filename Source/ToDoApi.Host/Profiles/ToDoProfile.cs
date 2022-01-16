using AutoMapper;

using Microsoft.Extensions.Logging;

using ToDoApi.Application.ToDos.Dto;
using ToDoApi.Domain.ToDos;

namespace ToDoApi.Host.Profiles
{
    public class ToDoProfile: Profile
    {
        public ToDoProfile()
{
            CreateMap<ToDo, ToDoDto>();

            CreateMap<CreateToDoDto, ToDo>()
                .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(dest => dest.IsFinished, opt => opt.MapFrom(o => o.IsFinished ?? false))
                .ForMember(dest => dest.FinishedAt, opt => opt.MapFrom(o => o.IsFinished == true ? o.FinishedAt : null));

            CreateMap<UpdateToDoDto, ToDo>()
                .ForMember(dest => dest.LastModifiedTime, opt => opt.MapFrom(o => DateTime.Now));
        }
    }
}
