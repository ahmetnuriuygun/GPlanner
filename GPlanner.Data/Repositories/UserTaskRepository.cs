using GPlanner.Core.Model;
using GPlanner.Data;
using Microsoft.EntityFrameworkCore;
public class UserTaskRepository : IUserTaskRepository
{
    private readonly GPlannerDbContext _context;
    public UserTaskRepository(GPlannerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserTask>> GetUserTasksByUserIdAsync(int userId)
    {
        return await _context.UserTasks
            .Where(ut => ut.UserId == userId)
            .ToListAsync();
    }

    public async Task<bool> DeleteUserTaskAsync(int taskId)
    {
        var task = await _context.UserTasks.FindAsync(taskId);
        if (task != null)
        {
            _context.UserTasks.Remove(task);
            await _context.SaveChangesAsync();
            return true; // Deletion successful
        }
        return false; // Task not found
    }
}