using Microsoft.AspNetCore.Mvc;
using Hangfire;

namespace SimulatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulatorController : Controller
    {
        private readonly IBackgroundJobClient _jobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        public SimulatorController(IBackgroundJobClient jobClient, IRecurringJobManager recurringJobManager)
        {
            _jobClient = jobClient;
            _recurringJobManager = recurringJobManager;
        }
        [HttpGet]
        [Route("Recurring")]
        public string RecurringJobs()
        {
            _recurringJobManager.AddOrUpdate("recurrentjob",() => Console.WriteLine("Welcome user in Recurring Job Demo!"), Cron.Minutely);
            return "Welcome user in Recurring Job Demo!";
        }
    }
}
