using StockService.Data.Enums;
using StockService.Models;

namespace StockService.Data.IServices
{
    public interface ICreateOriginalOrderService
    {
        public Task ExecuteOriginalOrder(OriginalOrder originalorder);
        public Task ChangeInformationOfAStockUnit(OriginalOrder originalorder, User user);
        public Task CreateExecutedOrder(OriginalOrder originalorderModel);
        public Task ExecuteOrder(Order order, double Price);
        public Task<List<OriginalOrder>> GetCorrespondantOrders(OriginalOrder originalorderModel,OrderTypeEnum orderTypeEnum);
        public Task StoreRemainingQuantity(OriginalOrder originalorderModel, int quantityneeded);
        public Task CreateInMarketOrder(OriginalOrder originalorderModel);
        public Task<List<StockUnit>> GetAllCorrespondingStockUnits(OriginalOrder originalorderModel);
    }
}
