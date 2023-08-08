using SimulatorService.Models;

namespace SimulatorService.SyncDataServices.Http
{
    public interface IStockDataClient
    {
        Task PostOriginalOrderToStock(OriginalOrderCreateDto originalOrder);
        Task GetInformationFromStock();

    }
}
