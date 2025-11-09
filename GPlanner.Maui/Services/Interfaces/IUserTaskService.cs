using GPlanner.Core.Model;

namespace GPlanner.Maui.Interfaces
{
    public interface IUserTaskService
    {
        Task<List<UserTask>> GetTasksByUserIdAsync(int userId);
        Task<UserTask> CreateTaskAsync(UserTask newTask);
        Task<UserTask> UpdateTaskAsync(UserTask updatedTask);
        Task<bool> DeleteTaskAsync(int taskId);

    }
}