using AutoMapper;
using GPlanner.Core.Model;
using System.Globalization;
using GPlanner.Maui.Models; // Assuming TaskDisplay is defined here, or update namespace to GPlanner.Maui.Dto

namespace GPlanner.Maui.Mapping
{
    // NOTE: This assumes GPlanner.Maui.Dto/TaskDisplay is where your DTO is located. 
    // Please adjust the using statement if TaskDisplay is in GPlanner.Maui.Models.

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Define mapping from the API model (UserTask) to the UI model (TaskDisplay)
            CreateMap<UserTask, TaskDisplayModel>() // Assuming TaskDisplay is your DTO name
                                                    // Custom mapping for complex properties: Date to formatted string
                .ForMember(
                    dest => dest.DisplayDate,
                    opt => opt.MapFrom(src => src.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture))
                )

                // Custom mapping for Priority (int) to PriorityText (string)
                .ForMember(
                    dest => dest.PriorityText,
                    opt => opt.MapFrom(src => TaskDisplayModel.GetPriorityText(src.Priority))
                )

                // Custom mapping for Type (enum) to TaskType (string)
                .ForMember(
                    dest => dest.TaskType,
                    opt => opt.MapFrom(src => src.Type.ToString())
                )
                // --- FIX: ReverseMap works now ---
                .ReverseMap();

            // NOTE: The previous context suggested GPlanner.Maui.Dto/TaskDisplay, 
            // but the file names and history used GPlanner.Maui.Models/TaskDisplayModel.
            // I'm assuming TaskDisplayModel is the target class for consistency with earlier history.
        }
    }
}