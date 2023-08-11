using SimulatorService.Data.DTOs;

namespace SimulatorService.SyncDataServices.Http
{
    public interface IStockDataClient
    {
        Task PostOriginalOrderToStock(OriginalOrderCreateDto originalOrder);
        Task GetInformationFromStock();

    }
}
