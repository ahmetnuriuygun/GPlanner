using GPlanner.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using GPlanner.Core.Model;
namespace GPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanningController : ControllerBase
    {
        private readonly IGeminiPlanningRepository _geminiPlanningRepository;
        private readonly IUserTaskRepository _userTaskRepository;



        public PlanningController(IGeminiPlanningRepository geminiPlanningRepository, IUserTaskRepository userTaskRepository)
        {
            _geminiPlanningRepository = geminiPlanningRepository;
            _userTaskRepository = userTaskRepository;
        }

        [HttpPost("generate/{userId}")]
        public async Task<ActionResult<List<DailyPlanItem>>> GeneratePlan(int userId)
        {
            try
            {
                var tasks = (await _userTaskRepository.GetUserTasksByUserIdAsync(userId)).Where(t => !t.IsArchived).ToList();
                if (!tasks.Any())
                {
                    return BadRequest("No active tasks found for the user to generate a plan.");
                }

                var generatedPlan = await _geminiPlanningRepository.GenerateAndSavePlanAsync(userId, tasks);

                return Ok(generatedPlan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while generating the plan: {ex.Message}");
            }

        }

    }
}