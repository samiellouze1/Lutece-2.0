using SimulatorService.Data.DTOs;
using System.Data.SqlTypes;

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
        public async Task<List<List<int>>> GetInformationFromStock()
        {

            var stockIds= new List<string>() {"1", "2", "3", "4" };
            var userIds = new List<string>() { "1", "2", "3", "4" };
            var usernames = new List<string>() { "user1@gdp.com", "user2@gdp.com", "user3@gdp.com", "user4@gdp.com" };
            var passwords = new List<string>() { "User1123@", "User2123@", "User3123@","User4123@" };
            var StockUserDictionnary = new List<List<int>>();
            for (int i=0;i<4;i++)
            {
                StockUserDictionnary.Add(new List<int>());
                string apiUrl = $"{_configuration["StockService"]}" + "StockUnit";
                Console.WriteLine(apiUrl);
                for (int j=0;j<4;j++)
                {
                    var stockId = stockIds[i];
                    var userId = userIds[j];
                    var endpointUrl = $"{_configuration["StockService"]}StockUnit/{stockId}/{userId}";
                    HttpResponseMessage response = await _httpClient.GetAsync(endpointUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        int content = await response.Content.ReadFromJsonAsync<int>();
                        Console.WriteLine($"StockId: {stockId}");
                        Console.WriteLine($"UserId: {userId}");
                        Console.WriteLine($"StockUnitCount: {content}");
                        Console.WriteLine("--------------------------");
                        StockUserDictionnary[i].Add(content);
                    }
                    else
                    {
                        Console.WriteLine("API Call was not successfull, API call statuscode" + response.StatusCode);
                    }
                }
            }
            return StockUserDictionnary,
        }

        public Task PostOriginalOrderToStock(OriginalOrderCreateDto originalOrder)
        {
            throw new NotImplementedException();
        }
    }
}
