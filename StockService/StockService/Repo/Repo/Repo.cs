using StockService.Data;
using StockService.Models;
using StockService.Repo.IRepo;

namespace StockService.Repo.Repo
{
    public class OrderRepo: EntityBaseRepository<Order>,IOrderRepo
    {
        public OrderRepo(AppDbContext context):base(context)
        {
            
        }
    }
    public class UserRepo : EntityBaseRepository<User>, IUserRepo
    {
        public UserRepo(AppDbContext context) : base(context)
        {

        }
    }
    public class StockUnitRepo : EntityBaseRepository<StockUnit>, IStockUnitRepo
    {
        public StockUnitRepo(AppDbContext context) : base(context)
        {

        }
    }
    public class StockRepo : EntityBaseRepository<Stock>, IStockRepo
    {
        public StockRepo(AppDbContext context) : base(context)
        {

        }
    }
    public class OriginalOrderRepo : EntityBaseRepository<OriginalOrder>, IOriginalOrderRepo
    {
        public OriginalOrderRepo(AppDbContext context) : base(context)
        {

        }
    }
}
