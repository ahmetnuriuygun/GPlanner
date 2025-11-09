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

    public async Task<UserTask> CreateUserTaskAsync(UserTask newTask)
    {
        await _context.UserTasks.AddAsync(newTask);
        await _context.SaveChangesAsync();

        return newTask;
    }


    public async Task<bool> UpdateUserTaskAsync(UserTask updatedTask)
    {
        var existingTask = await _context.UserTasks.FindAsync(updatedTask.TaskId);

        if (existingTask == null)
        {
            return false;
        }
        _context.Entry(existingTask).CurrentValues.SetValues(updatedTask);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserTaskAsync(int taskId)
    {
        var task = await _context.UserTasks.FindAsync(taskId);
        if (task != null)
        {
            _context.UserTasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}