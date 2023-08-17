using SimulatorService.Data.DTOs;

namespace SimulatorService.SyncDataServices.Http
{
    public interface IHttpStockDataClient
    {
        Task PostOriginalOrderToStock(OriginalOrderCreateDto originalOrderCreateDto);
        Task<int> GetInformationFromStockUnit(string userId, string stockId);
        Task<double> GetInformationFromStock(string stockId);
        Task Authenticating(string userId);
        Task LoggingOut();
    }
}
