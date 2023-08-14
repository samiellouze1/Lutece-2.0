using StockService.Data.Enums;
using StockService.Models;

namespace StockService.Services.IServices
{
    public interface ICreateOriginalOrderService
    {
        public Task ExecuteOriginalOrder(OriginalOrder originalorder);
        public Task ChangeInformationOfAStockUnit(OriginalOrder originalorder, User user);
        public Task CreateExecutedOrder(OriginalOrder originalorderModel);
        public Task ExecuteOrder(Order order, double Price);
        public Task<List<OriginalOrder>> GetCorrespondantOrders(OriginalOrder originalorderModel,OriginalOrderTypeEnum orderTypeEnum);
        public Task StoreRemainingQuantity(OriginalOrder originalorderModel, int quantityneeded);
        public Task CreateInMarketOrder(OriginalOrder originalorderModel);
        public Task<List<StockUnit>> GetAllCorrespondingStockUnits(OriginalOrder originalorderModel);
        public Task ChangeInformationOfAStockUnit(OriginalOrder originalorder);
        public Task ChangeInformationOfAStockUnitFromUsertoUser(OriginalOrder originalorder, User thebuyer);


    }
}
