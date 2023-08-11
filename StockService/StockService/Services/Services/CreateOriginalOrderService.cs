using StockService.Data.Enums;
using StockService.Models;
using StockService.Repo.IRepo;
using StockService.Services.IServices;

namespace StockService.Services.Services
{
    public class CreateOriginalOrderService : ICreateOriginalOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IOriginalOrderRepo _originalOrderRepo;
        private readonly IStockUnitRepo _stockUnitRepo;
        public CreateOriginalOrderService(IOrderRepo orderRepo, IOriginalOrderRepo originalOrderRepo, IStockUnitRepo stockUnitRepo)
        {
            _orderRepo = orderRepo;
            _originalOrderRepo = originalOrderRepo;
            _stockUnitRepo = stockUnitRepo;
        }
        public async Task CreateInMarketOrder(OriginalOrder originalorderModel)
        {
            var newOrderMarket = new Order()
            {
                OrderStatus = OrderStatusEnum.Active,
                OriginalOrder = originalorderModel
            };
            await _orderRepo.AddAsync(newOrderMarket);
        }
        public async Task StoreRemainingQuantity(OriginalOrder originalorderModel, int quantityneeded)
        {
            originalorderModel.RemainingQuantity = quantityneeded;
            await _originalOrderRepo.SaveChangesAsync();
        }
        public async Task<List<OriginalOrder>> GetCorrespondantOrders(OriginalOrder originalorderModel,OrderTypeEnum orderTypeEnum)
        {
            var OriginalOrders = await _originalOrderRepo.GetAllAsync(oo => oo.Stock, oo => oo.Orders, oo => oo.User);
            var sellingOriginalOrdersList = OriginalOrders.
                                                            Where(oo => oo.OrderType == orderTypeEnum).
                                                            Where(oo => oo.Stock == originalorderModel.Stock).
                                                            Where(oo => oo.OriginalOrderStatus == OriginalOrderStatusEnum.Active).
                                                            OrderBy(oo => oo.Price).
                                                            ToList();
            if (orderTypeEnum == OrderTypeEnum.Buy) 
            { 
                sellingOriginalOrdersList=sellingOriginalOrdersList.OrderByDescending(o=>o.Price).ToList();
            }
            return sellingOriginalOrdersList;
        }
        public async Task ExecuteOrder(Order order, double Price)
        {
            order.OrderStatus = OrderStatusEnum.Executed;
            order.ExecutedPrice = Price;
            await _orderRepo.SaveChangesAsync();
        }
        public async Task CreateExecutedOrder(OriginalOrder originalorderModel)
        {
            //creation of order in executed state
            var newOrderExecuted = new Order()
            {
                OriginalOrder = originalorderModel,
                OrderStatus = OrderStatusEnum.Executed,
                DateExecution = DateTime.Now
            };
            await _orderRepo.AddAsync(newOrderExecuted);
        }
        public async Task ChangeInformationOfAStockUnit(OriginalOrder originalorder, User user)
        {
            var theseller = originalorder.User;
            var stockunit = theseller.StockUnits.Where(su => su.Stock == originalorder.Stock).Where(su => su.StockUnitStatus == StockUnitStatusEnum.InMarket).ToList()[0];
            stockunit.User = user;
            stockunit.StockUnitStatus = StockUnitStatusEnum.InStock;
            stockunit.DateBought = DateTime.Now;
            await _stockUnitRepo.SaveChangesAsync();
        }
        public async Task ExecuteOriginalOrder(OriginalOrder originalorder)
        {
            originalorder.OriginalOrderStatus = OriginalOrderStatusEnum.Executed;
            await _originalOrderRepo.SaveChangesAsync();
        }

        public async Task<List<StockUnit>> GetAllCorrespondingStockUnits(OriginalOrder originalorderModel)
        {
            var allstockunits = await _stockUnitRepo.GetAllAsync();
            var stockunitlist = allstockunits.
                Where(su => su.Stock == originalorderModel.Stock).
                Where(su => su.User == originalorderModel.User).
                ToList();
            return stockunitlist;
        }
    }
}
