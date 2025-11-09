using GPlanner.Core.Model;

namespace GPlanner.Maui.Interfaces
{
    public interface IUserTaskService
    {
        Task<List<UserTask>> GetTasksByUserIdAsync(int userId);

    }
}