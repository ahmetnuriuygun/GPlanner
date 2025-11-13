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

            if (userTasks == null)
            {
                return Ok(new List<UserTask>());
            }

            return Ok(userTasks);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] UserTask newTask)
        {
            if (newTask.UserId <= 0)
            {
                return BadRequest("User ID must be provided for task creation.");
            }

            newTask.TaskId = 0;

            var createdTask = await _userTaskRepository.CreateUserTaskAsync(newTask);

            if (createdTask == null)
            {
                return StatusCode(500, "Failed to create task in the database.");
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

            if (taskId <= 0)
            {
                return BadRequest("Invalid Task ID provided for update.");
            }

            bool updated = await _userTaskRepository.UpdateUserTaskAsync(updatedTask);

            if (!updated)
            {
                return NotFound($"Task with ID {taskId} not found.");
            }

            return NoContent();
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            bool deleted = await _userTaskRepository.DeleteUserTaskAsync(taskId);

            if (!deleted)
            {
                return NotFound($"Task with ID {taskId} was not found for deletion.");
            }

            return NoContent();
        }

        [HttpPost("archive/{taskId}")]
        public async Task<IActionResult> ArchiveTask(int taskId)
        {
            bool archived = await _userTaskRepository.ArchiveUserTaskAsync(taskId);

            if (!archived)
            {
                return NotFound($"Task with ID {taskId} was not found for archiving.");
            }

            return NoContent();
        }
    }
}
