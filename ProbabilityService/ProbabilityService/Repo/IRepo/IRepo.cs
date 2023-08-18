using ProbabilityService.Models;

namespace ProbabilityService.Repo.IRepo
{
    public interface IStockRepo: IEntityBaseRepository<Stock>
    {
    }
    public interface IStockTraceRepo : IEntityBaseRepository<StockTrace>
    {
    }
    public interface IProbabilityDistributionRepo :IEntityBaseRepository<ProbabilityDistributionUnit> 
    {
    }
}
