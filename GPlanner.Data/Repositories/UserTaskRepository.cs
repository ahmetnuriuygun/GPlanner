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
        var existingTask = await _context.UserTasks.AsNoTracking().FirstOrDefaultAsync(ut => ut.TaskId == updatedTask.TaskId);

        if (existingTask == null)
        {
            return false;
        }
        _context.UserTasks.Attach(existingTask);
        existingTask.Title = updatedTask.Title;
        existingTask.Description = updatedTask.Description;
        existingTask.Type = updatedTask.Type;
        existingTask.Date = updatedTask.Date;
        existingTask.Priority = updatedTask.Priority;
        existingTask.IsArchived = updatedTask.IsArchived;
        _context.Entry(existingTask).State = EntityState.Modified;
        _context.Entry(existingTask).Property(t => t.UserId).IsModified = false;
        _context.Entry(existingTask).Property(t => t.TaskId).IsModified = false;


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

    public async Task<bool> ArchiveUserTaskAsync(int taskId)
    {
        var task = await _context.UserTasks.FindAsync(taskId);
        if (task != null)
        {
            task.IsArchived = true;
            _context.UserTasks.Update(task);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}