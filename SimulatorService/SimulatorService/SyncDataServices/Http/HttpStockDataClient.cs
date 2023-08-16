using SimulatorService.Data.DTOs;
using System.Data.SqlTypes;
using System.Text;
using System.Text.Json;

namespace SimulatorService.SyncDataServices.Http
{
    public class HttpStockDataClient : IHttpStockDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpStockDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<double> GetInformationFromStock(string stockId)
        {
            string endpointUrl = $"{_configuration["StockService"]}Stock/{stockId}";
            HttpResponseMessage response = await _httpClient.GetAsync(endpointUrl);
            if (response.IsSuccessStatusCode)
            {
                var stockreaddto = await response.Content.ReadFromJsonAsync<StockReadDTO>();
                return stockreaddto.AveragePrice; 
            }
            else
            {
                throw new HttpRequestException("Failed to retrieve stock information from the service.");
            }
        }

        public async Task<int> GetInformationFromStockUnit(string stockId, string userId)
        {
            string endpointUrl = $"{_configuration["StockService"]}" + "StockUnit/StockUser";
            var stockuserdto = new StockUserDTO()
            {
                StockId = stockId,
                UserId = userId
            };
            var content = new StringContent(
                JsonSerializer.Serialize(stockuserdto),
                Encoding.UTF8,
                "application/json"
                );
            HttpResponseMessage response = await _httpClient.PostAsync(endpointUrl, content);
            var extract = new int();
            if (response.IsSuccessStatusCode)
            {
                extract = await response.Content.ReadFromJsonAsync<int>();
            }
            else
            {
                Console.WriteLine("API Call was not successfull, API call statuscode" + response.StatusCode);
            }
            return extract;
        }
        public async Task PostOriginalOrderToStock(OriginalOrderCreateDto originalOrderCreateDto)
        {
            string endpointUrl = $"{_configuration["StockService"]}" + "OriginalOrder";
            var content = new StringContent(
                JsonSerializer.Serialize(originalOrderCreateDto),
                Encoding.UTF8,
                "application/json"
                );
            HttpResponseMessage response= await _httpClient.PostAsync(endpointUrl,content);
            if (response.IsSuccessStatusCode) 
            {
                Console.WriteLine("posting original order Successfull");
            }
            else
            {
                Console.WriteLine("API call not successfull for posting original order");
            }
        }

    }
}
