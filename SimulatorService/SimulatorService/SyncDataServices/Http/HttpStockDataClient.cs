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
        public async Task GetInformationFromStock()
        {
            string apiUrl= $"{_configuration["StockService"]}" + "Stock";
            Console.WriteLine(apiUrl);
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                List<StockReadDTO> stockList = await response.Content.ReadFromJsonAsync<List<StockReadDTO>>();
                foreach (var stockitem in stockList)
                {
                    //lezem nekhthou el stockunits
                    //nekhthou stock random
                    //nekhthou mennou proprietaire random
                    //nbi3ou mennou quantite random

                    //lezem nekhthou stock random
                    //nekhthou proprietaire random 
                    //nechriwlou quantite random

                    Console.WriteLine("&&&&&&&&&&&&&&");
                }
            }
            else
            {
                Console.WriteLine("API request was not successfull. Status Code: "+ response.StatusCode);
            }
        }

        public Task PostOriginalOrderToStock(OriginalOrderCreateDto originalOrder)
        {
            throw new NotImplementedException();
        }
    }
}
