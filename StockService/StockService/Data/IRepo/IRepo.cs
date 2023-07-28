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
    public interface IStockUnitRepo : IEntityBaseRepository<StockUnit>
    {
    }
    public interface IStockRepo : IEntityBaseRepository<Stock>
    {
    }

}
