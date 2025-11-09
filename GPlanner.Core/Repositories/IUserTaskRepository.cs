
using GPlanner.Core.Model;
public interface IUserTaskRepository
{
    Task<IEnumerable<UserTask>> GetUserTasksByUserIdAsync(int userId);
}