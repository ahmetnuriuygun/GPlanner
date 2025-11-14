public class ScheduledTaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ActivityType { get; set; } = string.Empty;
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Description { get; set; } = string.Empty;
    public string OriginalTaskTitle { get; set; } = string.Empty;
    public int? OriginalUserTaskId { get; set; }
}