using CommunityToolkit.Mvvm.ComponentModel;
using GPlanner.Core.Model;
namespace GPlanner.Maui.Models
{
    public partial class TaskDisplayModel : ObservableObject
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }

        [ObservableProperty]
        public string title = string.Empty;

        [ObservableProperty]
        public string description = string.Empty;

        [ObservableProperty]
        public string displayDate = string.Empty;

        [ObservableProperty]
        public string priorityText = string.Empty;

        [ObservableProperty]
        public string taskType = string.Empty;

        public static string GetPriorityText(int priority) => priority switch
        {
            5 => "High",
            4 => "Medium",
            3 => "Low"
        };
    }
}