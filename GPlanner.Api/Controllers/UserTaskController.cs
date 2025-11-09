using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
    }
}