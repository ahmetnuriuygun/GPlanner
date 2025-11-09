using GPlanner.Core.Model;
namespace GPlanner.Maui.Services.Dtos
{
    public class UserTaskUpdateDto : UserTaskBaseDto
    {
        public int TaskId { get; set; }

        public int UserId { get; set; }

    }
}