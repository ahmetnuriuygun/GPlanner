using GPlanner.Core.Model;

namespace GPlanner.Maui.Interfaces
{
    public interface IUserTaskService
    {
        Task<List<UserTask>> GetTasksByUserIdAsync(int userId);
        Task<bool> DeleteTaskAsync(int taskId);

        Task<bool> CreateTaskAsync(UserTask newTask);
        Task<bool> UpdateTaskAsync(UserTask updatedTask);

        Task<bool> ArchiveTaskAsync(int taskId);

    }
}