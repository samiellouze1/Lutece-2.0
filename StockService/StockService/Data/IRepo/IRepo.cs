using StockService.Models;

namespace StockService.Data.IRepo
{
    public interface IOrderRepo: IEntityBaseRepository<Order>
    {
    }
    public interface IOriginalOrderRepo : IEntityBaseRepository<OriginalOrder>
    {
    }
    public interface IUserRepo : IEntityBaseRepository<User>
    {
    }
    public interface IStockUserRepo : IEntityBaseRepository<StockUser>
    {
    }
    public interface IStockRepo : IEntityBaseRepository<Stock>
    {
    }

}
