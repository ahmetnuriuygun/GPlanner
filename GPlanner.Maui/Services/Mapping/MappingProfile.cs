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
            CreateMap<DaillyPlanDto, DailyPlanItem>()
                           .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks));

            CreateMap<ScheduledTaskDto, ScheduledTask>()
                .ForMember(dest => dest.DailyPlanItem, opt => opt.Ignore());
        }
    }
}