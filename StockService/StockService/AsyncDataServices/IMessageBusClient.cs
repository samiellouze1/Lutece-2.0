using StockService.Data.DTOs;

namespace StockService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        public void PublishNewStockPrice(StockPriceDTO stockprice);
    }
}
