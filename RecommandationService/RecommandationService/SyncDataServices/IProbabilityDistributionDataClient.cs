using RecommandationService.Models;

namespace RecommandationService.SyncDataServices
{
    public interface IProbabilityDistributionDataClient
    {
        IEnumerable<ProbabilityDistributionUnit> ReturnAllProbabilityDistributionUnits ();
    }
}
