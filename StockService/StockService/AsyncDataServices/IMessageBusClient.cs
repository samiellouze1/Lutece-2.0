using StockService.Data.DTOs;

namespace StockService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        public void PublishNewStock(StockPublishDTO stockPublishDTO);
    }
}
