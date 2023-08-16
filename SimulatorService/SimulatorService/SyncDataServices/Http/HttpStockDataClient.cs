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
        public async Task Authenticating(string userId)
        {
            Console.WriteLine("--------------authenticating-----------");
            var username = $"user{userId}@gdp.com";
            var password = $"User{userId}123@";
            var login = new LoginDTO()
            {
                UserName=username,
                Password=password
            };
            string endpointUrl = $"{_configuration["StockService"]}User/Login";
            var content = new StringContent
                (
                    JsonSerializer.Serialize(login),
                    Encoding.UTF8,
                    "application/json"
                );
            HttpResponseMessage response = await _httpClient.PostAsync(endpointUrl, content); 
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("-----------------authenticated------------");
            }
            else 
            { 
                Console.WriteLine(response.StatusCode.ToString());
            }
        }
        public async Task LoggingOut()
        {
            Console.WriteLine("---------------logging out--------");
            string endpointUrl = $"{_configuration["StockService"]}User/Logout";
            HttpResponseMessage response = await _httpClient.GetAsync(endpointUrl);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("---------------logged out --------");
            }
            else
            {
                Console.WriteLine(response.StatusCode.ToString());   
            }
        }
        public async Task<double> GetInformationFromStock(string stockId)
        {
            Console.WriteLine($"-----------------getting information from stock {stockId}-----------------------");
            string endpointUrl = $"{_configuration["StockService"]}Stock/{stockId}";
            HttpResponseMessage response = await _httpClient.GetAsync(endpointUrl);
            if (response.IsSuccessStatusCode)
            {
                var stockreaddto = await response.Content.ReadFromJsonAsync<StockReadDTO>();
                Console.WriteLine($"----------------getting information successfull avgprice:{stockreaddto.AveragePrice}-----------");
                return stockreaddto.AveragePrice; 
            }
            else
            {
                throw new HttpRequestException("Failed to retrieve stock information from the service.");
            }
        }

        public async Task<int> GetInformationFromStockUnit(string stockId, string userId)
        {
            Console.WriteLine($"----------------getting stock information for stockid {stockId} and userid {userId}------------------------");
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
                Console.WriteLine($"----------------got stock information for stockid {stockId} and userid {userId}------------------------");
                extract = await response.Content.ReadFromJsonAsync<int>();
                Console.WriteLine($"----------------extract:{extract}");
            }
            else
            {
                Console.WriteLine("API Call was not successfull, API call statuscode" + response.StatusCode);
            }
            return extract;
        }
        public async Task PostOriginalOrderToStock(OriginalOrderCreateDto originalOrderCreateDto)
        {
            Console.WriteLine($"----------------posting original ordercreatedto------------------------");
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
