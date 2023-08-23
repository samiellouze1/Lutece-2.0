using Hangfire;
using Microsoft.AspNetCore.Mvc;
using ProbabilityService.SyncDataServices.Http;

namespace ProbabilityService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProbabilityController:Controller
    {
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IHttpProbabilityDataClient _httpProbabilityDataClient;
        public ProbabilityController(IRecurringJobManager recurringJobManager, IHttpProbabilityDataClient httpProbabilityDataClient)
        {
            _httpProbabilityDataClient = httpProbabilityDataClient;
            _recurringJobManager = recurringJobManager;
        }
        [HttpGet]
        [Route("Recurring")]
        public string RecurringJobs()
        {
            Console.WriteLine("-----------recurring awakend --------");
            _recurringJobManager.AddOrUpdate("recurrentjob", () => _httpProbabilityDataClient.GetProbabilityDistribution(),Cron.MinuteInterval(5));
            return "Updated probability distribution";
        }
    }
}
