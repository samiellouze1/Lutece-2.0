using StockService.Models;

namespace StockService.Data.IRepo
{
    public interface IOrderRepo
    {
        bool SaveChanges();
        IEnumerable<Order> GetAll();
        Order GetOrderById(int id);
        void CreateOrder (Order order);
    }
}
