using SimulatorService.Data.DTOs;

namespace SimulatorService.SyncDataServices.Http
{
    public class HttpStockDataClient : IStockDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpStockDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public Task GetInformationFromStock()
        {
            throw new NotImplementedException();
        }

        public Task PostOriginalOrderToStock(OriginalOrderCreateDto originalOrder)
        {
            throw new NotImplementedException();
        }
    }
}
