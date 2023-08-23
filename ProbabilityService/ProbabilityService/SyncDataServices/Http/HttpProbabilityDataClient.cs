using ProbabilityService.Models;
using ProbabilityService.Repo.IRepo;
using System.Data.SqlTypes;

namespace ProbabilityService.SyncDataServices.Http
{
    public class HttpProbabilityDataClient : IHttpProbabilityDataClient
    {
        private readonly IStockRepo _stockRepo;
        public Task<List<ProbabilityDistributionUnit>> GetProbabilityDistribution()
        {
            throw new NotImplementedException();
        }
    }
}
