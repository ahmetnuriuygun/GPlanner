using CommunityToolkit.Mvvm.ComponentModel;
using GPlanner.Core.Model;
using System.Text.Json.Serialization;
namespace GPlanner.Core.Model
{
    public partial class ScheduledTask : ObservableObject
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ActivityType { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }

        [JsonPropertyName("originalTaskTitle")]
        public string OriginalTaskTitle { get; set; } = string.Empty;

        public int? OriginalUserTaskId { get; set; }
        [JsonIgnore]
        public DailyPlanItem DailyPlanItem { get; set; }

        public DateTime DailyPlanDate { get; set; }

        public ScheduledTask()
        {

        }
    }
}