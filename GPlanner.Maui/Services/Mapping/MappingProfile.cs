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
            // Maps the full entity from the API to the full Read DTO in MAUI.
            CreateMap<UserTask, UserTaskReadDto>();

            // Maps Read DTO to Update DTO for modal initialization (Edit Mode).
            CreateMap<UserTaskReadDto, UserTaskUpdateDto>();

            // CRITICAL: Maps the base DTO (ActiveTask property) to the Update DTO, 
            // ensuring all latest UI changes are captured before saving.
            CreateMap<UserTaskBaseDto, UserTaskUpdateDto>();

            // NEW: Maps the Update DTO back to the Create DTO for New Task flow initialization.
            CreateMap<UserTaskUpdateDto, UserTaskCreateDto>();


            // Maps the Create DTO back to the Entity for the API POST (New Task).
            CreateMap<UserTaskCreateDto, UserTask>()
                .ForMember(dest => dest.TaskId, opt => opt.Ignore()) // TaskId is generated on server
                .ForMember(dest => dest.IsArchived, opt => opt.Ignore());

            // Maps the Update DTO back to the Entity for the API PUT (Saving Task).
            CreateMap<UserTaskUpdateDto, UserTask>();

            // (Redundant but harmless mapping)
            CreateMap<UserTask, UserTaskUpdateDto>();
        }
    }
}