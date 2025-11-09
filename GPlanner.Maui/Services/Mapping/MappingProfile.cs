using AutoMapper;
using GPlanner.Core.Model;
using System.Globalization;
using GPlanner.Maui.Services.Dtos;

namespace GPlanner.Maui.Mapping
{


    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserTask, UserTaskReadDto>();

            CreateMap<UserTaskCreateDto, UserTask>()
                .ForMember(dest => dest.TaskId, opt => opt.Ignore())
                .ForMember(dest => dest.IsArchived, opt => opt.Ignore());

            CreateMap<UserTaskUpdateDto, UserTask>();

            CreateMap<UserTask, UserTaskUpdateDto>();
        }
    }
}