using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPlanner.Core.Model;
namespace GPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyPlanController : ControllerBase
    {

        private readonly IDailyPlanRepoistory _dailyPlanRepository;

        public DailyPlanController(IDailyPlanRepoistory dailyPlanRepository)
        {
            _dailyPlanRepository = dailyPlanRepository;
        }

        public async Task<IActionResult> GetDailyPlans()
        {
            var dailyPlans = await _dailyPlanRepository.GetDailyPlansAsync();
            return Ok(dailyPlans);
        }
    }
}