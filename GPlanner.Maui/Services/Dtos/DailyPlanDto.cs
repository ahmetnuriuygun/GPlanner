public class DaillyPlanDto
{
    public DateTime Date { get; set; }
    public string DayOfWeek { get; set; } = string.Empty;
    public List<ScheduledTaskDto> Tasks { get; set; } = new();
}