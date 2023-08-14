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
        public async Task<List<OriginalOrder>> GetCorrespondantOrders(OriginalOrder originalorderModel,OriginalOrderTypeEnum orderTypeEnum)
        {
            var OriginalOrders = await _originalOrderRepo.GetAllAsync(oo => oo.Stock.StockUnits, oo => oo.Orders, oo => oo.User.StockUnits);
            var sellingOriginalOrdersList = OriginalOrders.
                                                            Where(oo => oo.OriginalOrderType != orderTypeEnum).
                                                            Where(oo => oo.StockId == originalorderModel.StockId).
                                                            Where(oo => oo.OriginalOrderStatus == OriginalOrderStatusEnum.Active).
                                                            ToList();
            if (orderTypeEnum == OriginalOrderTypeEnum.Buy) 
            { 
                sellingOriginalOrdersList=sellingOriginalOrdersList.
                    Where(oo=>oo.Price<originalorderModel.Price).
                    OrderBy(o=>o.Price).
                    OrderByDescending(o=>o.DateDeposit).
                    ToList();
            }
            else
            {
                sellingOriginalOrdersList = sellingOriginalOrdersList.
                    Where(oo => oo.Price > originalorderModel.Price).
                    OrderByDescending(o => o.Price).
                    OrderByDescending(o => o.DateDeposit).
                    ToList();
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
        public async Task ChangeInformationOfAStockUnit(OriginalOrder originalorder, User thebuyer)
        {
            var theseller = originalorder.User;
            var stockunits = theseller.StockUnits.Where(su => su.Stock == originalorder.Stock).ToList();
            stockunits = stockunits.Where(su => su.StockUnitStatus == StockUnitStatusEnum.InMarket).ToList();
            var stockunit = stockunits[0];
            stockunit.User = thebuyer;
            stockunit.StockUnitStatus = StockUnitStatusEnum.InStock;
            stockunit.DateBought = DateTime.Now;
            await _stockUnitRepo.SaveChangesAsync();
        }
        public async Task ChangeInformationOfAStockUnitFromUsertoUser(OriginalOrder originalorder, User thebuyer)
        {
            var theseller = originalorder.User;
            var stockunits = theseller.StockUnits.Where(su => su.Stock == originalorder.Stock).ToList();
            stockunits = stockunits.Where(su => su.StockUnitStatus == StockUnitStatusEnum.InStock).ToList();
            var stockunit = stockunits[0];
            stockunit.User = thebuyer;
            stockunit.DateBought = DateTime.Now;
            await _stockUnitRepo.SaveChangesAsync();
        }
        public async Task ChangeInformationOfAStockUnit(OriginalOrder originalorder)
        {
            var theowner = originalorder.User;
            var stockunits = theowner.StockUnits.Where(su => su.Stock == originalorder.Stock).ToList();
            stockunits=stockunits.Where(su => su.StockUnitStatus == StockUnitStatusEnum.InStock).OrderBy(su=>su.DateBought).ToList();
            var stockunit = stockunits[0];
            stockunit.StockUnitStatus = StockUnitStatusEnum.InMarket;
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
                Where(su=>su.StockUnitStatus==StockUnitStatusEnum.InStock).
                ToList();
            return stockunitlist;
        }
    }
}
