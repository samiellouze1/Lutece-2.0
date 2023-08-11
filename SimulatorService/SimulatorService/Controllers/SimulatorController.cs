using Microsoft.AspNetCore.Mvc;
using Hangfire;
using SimulatorService.SyncDataServices.Http;

namespace SimulatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulatorController : Controller
    {
        private readonly IBackgroundJobClient _jobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IStockDataClient _stockDataClient;
        public SimulatorController(IBackgroundJobClient jobClient, IRecurringJobManager recurringJobManager, IStockDataClient stockDataClient)
        {
            _jobClient = jobClient;
            _recurringJobManager = recurringJobManager;
            _stockDataClient = stockDataClient;
        }
        [HttpGet]
        [Route("Recurring")]
        public string RecurringJobs()
        {
            _recurringJobManager.AddOrUpdate("recurrentjob",() => _stockDataClient.GetInformationFromStock(), Cron.Minutely);
            return "Welcome user in Recurring Job Demo!";
        }
    }
}
