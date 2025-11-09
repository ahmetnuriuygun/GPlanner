using GPlanner.Core.Model;
namespace GPlanner.Maui.Services.Dtos
{
    public class UserTaskReadDto
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public TaskType Type { get; set; }
        public DateTime Date { get; set; }
        // 🟢 FIX: Add missing fields for edit functionality
        public string Description { get; set; } = string.Empty;
        public int Priority { get; set; }
        public int UserId { get; set; } // Also needed for update flow


    }
}