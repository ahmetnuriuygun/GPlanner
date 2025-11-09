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
}