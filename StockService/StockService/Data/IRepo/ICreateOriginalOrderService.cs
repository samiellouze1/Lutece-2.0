using StockService.Models;

namespace StockService.Data.IRepo
{
    public interface ICreateOriginalOrderService
    {
        public Task ExecuteOriginalOrder(OriginalOrder originalorder);
        public  Task ChangeInformationOfAStockUnit(OriginalOrder originalorder, User user);
        public  Task CreateExecutedOrder(OriginalOrder originalorderModel);
        public  Task ExecuteOrder(Order order, double Price);
        public  Task<List<OriginalOrder>> GetCorrespondantSellingOrders(OriginalOrder originalorderModel);
        public  Task StoreRemainingQuantity(OriginalOrder originalorderModel, int quantityneeded);
        public Task CreateInMarketOrder(OriginalOrder originalorderModel);
    }
}
