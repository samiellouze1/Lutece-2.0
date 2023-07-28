using StockService.Data.IRepo;
using StockService.Data.Repository;
using StockService.Models;

namespace StockService.Data.Repo
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
    public class StockUserRepo : EntityBaseRepository<StockUser>, IStockUserRepo
    {
        public StockUserRepo(AppDbContext context) : base(context)
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
