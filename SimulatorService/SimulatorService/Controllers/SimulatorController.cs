using Microsoft.AspNetCore.Mvc;
using Hangfire;
using SimulatorService.SyncDataServices.Http;
using SimulatorService.Randomizer;

namespace SimulatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulatorController : Controller
    {
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IRandomizer _randomizer;
        public SimulatorController( IRecurringJobManager recurringJobManager, IRandomizer randomizer)
        {
            _recurringJobManager = recurringJobManager;
            _randomizer = randomizer;
        }
        [HttpGet]
        [Route("Recurring")]
        public string RecurringJobs()
        {
            _recurringJobManager.AddOrUpdate("recurrentjob",() => _randomizer.RandomizeOriginalOrderSell(), Cron.Minutely);
            return "Randomized an original order";
        }
    }
}
