using GPlanner.Core.Model;
public interface IUserTaskRepository
{
    Task<IEnumerable<UserTask>> GetUserTasksByUserIdAsync(int userId);
    Task<bool> DeleteUserTaskAsync(int taskId);

    Task<UserTask> CreateUserTaskAsync(UserTask newTask);
    Task<bool> UpdateUserTaskAsync(UserTask updatedTask);
}