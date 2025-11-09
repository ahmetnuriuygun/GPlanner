using GPlanner.Core.Model;
namespace GPlanner.Maui.Services.Dtos;

public abstract class UserTaskBaseDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskType Type { get; set; }
    public DateTime Date { get; set; }
    public int Priority { get; set; }
}
