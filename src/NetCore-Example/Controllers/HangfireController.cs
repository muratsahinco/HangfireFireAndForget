using Common.Hangfire.Schedules;
using Microsoft.AspNetCore.Mvc;

namespace NetCore_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : Controller
    {
        [HttpGet("StartTask")]
        public async Task<IActionResult> StartTask(int TempParam)
        {
            TempSchedules.AddTemp(TempParam);
            return Ok();
        }
    }
}
