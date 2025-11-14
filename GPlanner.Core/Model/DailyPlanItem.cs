using CommunityToolkit.Mvvm.ComponentModel;
namespace GPlanner.Core.Model
{
    public partial class DailyPlanItem : ObservableObject
    {
        public DateTime Date { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;
        public List<ScheduledTask> Tasks { get; set; } = new List<ScheduledTask>();

        public DateTimeOffset? LastGenerated { get; set; }

        public DailyPlanItem()
        {

        }
    }
}