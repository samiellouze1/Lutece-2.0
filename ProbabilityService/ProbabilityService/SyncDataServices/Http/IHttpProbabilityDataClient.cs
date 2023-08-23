using ProbabilityService.Models;

namespace ProbabilityService.SyncDataServices.Http
{
    public interface IHttpProbabilityDataClient
    {
        Task<List<ProbabilityDistributionUnit>> GetProbabilityDistribution();
    }
}
