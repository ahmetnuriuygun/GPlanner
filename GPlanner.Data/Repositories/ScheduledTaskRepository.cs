using GPlanner.Data;
using GPlanner.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPlanner.Core.Repositories;

public class ScheduledTaskRepository : IScheduledTaskRepository
{
    private readonly GPlannerDbContext _context;

    public ScheduledTaskRepository(GPlannerDbContext context)
    {
        _context = context;
    }

    public async Task SaveDailyPlanAsync(List<DailyPlanItem> dailyPlanItems)
    {
        if (dailyPlanItems == null || dailyPlanItems.Count == 0)
            return;

        var startDate = dailyPlanItems.Min(d => d.Date);
        var endDate = dailyPlanItems.Max(d => d.Date);

        var existingPlans = await _context.DailyPlanItems
            .Include(d => d.Tasks) // Include ScheduledTasks
            .Where(d => d.Date >= startDate && d.Date <= endDate)
            .ToListAsync();

        if (existingPlans.Any())
        {
            var existingTasks = existingPlans.SelectMany(d => d.Tasks).ToList();
            _context.ScheduledTasks.RemoveRange(existingTasks);

            _context.DailyPlanItems.RemoveRange(existingPlans);

            await _context.SaveChangesAsync();
        }

        await _context.DailyPlanItems.AddRangeAsync(dailyPlanItems);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateScheduledTaskAsync(int taskId)
    {
        var existingTask = await _context.ScheduledTasks
                   .AsNoTracking()
                   .FirstOrDefaultAsync(t => t.Id == taskId);
        if (existingTask != null)
        {
            existingTask.IsCompleted = !existingTask.IsCompleted;
            _context.ScheduledTasks.Attach(existingTask);

            _context.Entry(existingTask).Property(t => t.IsCompleted).IsModified = true;
            await _context.SaveChangesAsync();
        }
    }
}
