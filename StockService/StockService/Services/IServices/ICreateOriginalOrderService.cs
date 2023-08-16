using StockService.Data.Enums;
using StockService.Models;

namespace StockService.Services.IServices
{
    public interface ICreateOriginalOrderService
    {
        public Task<List<OriginalOrder>> GetCorrespondantOrders(OriginalOrder originalorderModel);
        public Task ExecuteOriginalOrder(OriginalOrder originalorder); 
        public Task ExecuteOrder(Order order, double Price);
        public Task CreateExecutedOrder(OriginalOrder originalorderModel);
        public Task CreateInMarketOrder(OriginalOrder originalorderModel);
        public Task StoreRemainingQuantity(OriginalOrder originalorderModel, int quantityneeded);
        public Task<List<StockUnit>> GetAllCorrespondingStockUnits(OriginalOrder originalorderModel);
        public Task ChangeInformationOfAStockUnitFromInStockToInMarket(OriginalOrder originalorder);
        public Task ChangeInformationOfAStockUnitFromInMarkettoInStock(OriginalOrder originalorder, User user);
        public Task ChangeInformationOfAStockUnitFromUsertoUser(OriginalOrder originalorder, User thebuyer);
        public Task UpdateStockAveragePrice(OriginalOrder originalorderModel);
    }
}
