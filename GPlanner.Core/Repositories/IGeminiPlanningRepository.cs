using GPlanner.Core.Model;

namespace GPlanner.Core.Repositories
{
    public interface IGeminiPlanningRepository
    {
        Task<List<DailyPlanItem>> GenerateAndSavePlanAsync(int userId, List<UserTask> tasks);
    }
}