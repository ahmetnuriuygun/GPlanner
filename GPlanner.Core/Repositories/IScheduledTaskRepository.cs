using GPlanner.Core.Model;
namespace GPlanner.Core.Repositories
{
    public interface IScheduledTaskRepository
    {
        Task SaveDailyPlanAsync(List<DailyPlanItem> dailyPlanItems);
        Task UpdateScheduledTaskAsync(int taskId);
    }
}