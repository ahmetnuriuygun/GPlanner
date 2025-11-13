// File: GeminiPlanModels.cs
namespace GPlanner.Core.Model
{
    public class GeminiPlanWrapper
    {
        public List<GeminiDailyPlanItem> DailyPlanItems { get; set; } = new();
    }

    public class GeminiDailyPlanItem
    {
        public DateTime Date { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;

        public DateTime LastGenerated { get; set; }

        public List<GeminiScheduledTask> Tasks { get; set; } = new();


    }

    public class GeminiScheduledTask
    {
        public string Title { get; set; } = string.Empty;
        public string ActivityType { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
        public string OriginalTaskTitle { get; set; } = string.Empty;
        public int OriginalUserTaskId { get; set; }
    }
}
