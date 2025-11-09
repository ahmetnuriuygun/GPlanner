using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPlanner.Core.Model;

namespace GPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
        private readonly IUserTaskRepository _userTaskRepository;

        public UserTaskController(IUserTaskRepository userTaskRepository)
        {
            _userTaskRepository = userTaskRepository;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<UserTask>>> GetTasksByUserId(int userId)
        {
            var userTasks = await _userTaskRepository.GetUserTasksByUserIdAsync(userId);

            if (userTasks == null || !userTasks.Any())
            {
                return Ok(new List<UserTask>());
            }

            return Ok(userTasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] UserTask newTask)
        {
            var createdTask = await _userTaskRepository.CreateUserTaskAsync(newTask);

            if (createdTask == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetTasksByUserId), new { userId = createdTask.UserId }, createdTask);
        }


        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTask(int taskId, [FromBody] UserTask updatedTask)
        {
            if (taskId != updatedTask.TaskId)
            {
                return BadRequest($"Task ID in the route ({taskId}) does not match the ID in the body ({updatedTask.TaskId}).");
            }

            bool updated = await _userTaskRepository.UpdateUserTaskAsync(updatedTask);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            bool deleted = await _userTaskRepository.DeleteUserTaskAsync(taskId);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
