using GPlanner.Data;
using GPlanner.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using GPlanner.Core.Model;
public class DailyPlanRepository : IDailyPlanRepoistory
{
    private readonly GPlannerDbContext _context;
    public DailyPlanRepository(GPlannerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DailyPlanItem>> GetDailyPlansAsync()
    {
        return await _context.DailyPlanItems
                    .Include(dpi => dpi.Tasks)
                    .ToListAsync();
    }
}