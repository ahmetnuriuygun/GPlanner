using GPlanner.Core.Model;
public interface IToDoService
{
    Task<List<DailyPlanItem>> GetDailyPlansAsync();

}