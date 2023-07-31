using OrderSimulator.Models;

namespace OrderSimulator.SyncDataServices.Grpc
{
    public interface IStockDataClient
    {
        IEnumerable<Stock> ReturnAllStocks();
    }
}
