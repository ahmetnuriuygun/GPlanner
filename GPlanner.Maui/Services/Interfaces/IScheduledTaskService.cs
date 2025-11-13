public interface IScheduledTaskService
{
    Task<bool> UpdateScheduledTaskAsync(int taskId);
}