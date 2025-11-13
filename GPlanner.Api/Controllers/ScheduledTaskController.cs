using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPlanner.Core.Model;
using GPlanner.Core.Repositories;

namespace GPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduledTaskController : ControllerBase
    {

        private readonly IScheduledTaskRepository _scheduledTaskRepository;

        public ScheduledTaskController(IScheduledTaskRepository scheduledTaskRepository)
        {
            _scheduledTaskRepository = scheduledTaskRepository;
        }

        [HttpPut("completed/{taskId}")]
        public async Task<IActionResult> UpdateScheduledTask(int taskId)
        {
            if (taskId <= 0)
            {
                return BadRequest("Invalid task data provided.");
            }
            await _scheduledTaskRepository.UpdateScheduledTaskAsync(taskId);
            return NoContent();
        }


    }

}