using StockService.Data.IRepo;
using StockService.Models;

namespace StockService.Data.Repo
{
    public class OrderRepo : IOrderRepo
    {
        private readonly AppDbContext _context;
        public OrderRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            else
            {
                _context.Orders.Add(order);
            }
        }
        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }
        public Order GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(x => x.Id == id);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);        
        }
    }
}
