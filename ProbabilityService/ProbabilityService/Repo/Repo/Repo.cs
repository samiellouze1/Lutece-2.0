using ProbabilityService.Data;
using ProbabilityService.Models;
using ProbabilityService.Repo.IRepo;

namespace ProbabilityService.Repo.Repo
{
    public class StockRepo :EntityBaseRepository<Stock>,IStockRepo
    {
        public StockRepo(AppDbContext context):base(context) 
        {
        }
    }
    public class StockTraceRepo :EntityBaseRepository<StockTrace>,IStockTraceRepo
    {
        public StockTraceRepo(AppDbContext context):base(context) 
        { 
        }
    }
    public class ProbabilityDistributionUnitRepo :EntityBaseRepository<ProbabilityDistributionUnit>,IProbabilityDistributionRepo
    {
        public ProbabilityDistributionUnitRepo(AppDbContext context) : base(context) 
        {
        }
    }
}
