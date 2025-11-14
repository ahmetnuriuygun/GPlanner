using GPlanner.Core.Model;

public interface IDailyPlanRepoistory
{
    Task<IEnumerable<DailyPlanItem>> GetDailyPlansAsync();

}