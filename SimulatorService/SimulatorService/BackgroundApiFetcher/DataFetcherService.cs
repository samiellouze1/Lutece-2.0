namespace SimulatorService.BackgroundApiFetcher
{
    public class DataFetcherService : BackgroundService
    {
        private readonly ILogger<DataFetcherService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public DataFetcherService(ILogger<DataFetcherService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Fetching and processing data from the external API...");

                // Perform the API request and data processing here
                await FetchAndProcessDataAsync();

                _logger.LogInformation("Data fetch and processing completed.");

                // Wait for an hour before fetching data again
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        private async Task FetchAndProcessDataAsync()
        {
            try
            {
                // Create an HttpClient using the HttpClientFactory
                var httpClient = _httpClientFactory.CreateClient();

                // Make the API request
                var apiResponse = await httpClient.GetAsync("https://api.example.com/data");

                if (apiResponse.IsSuccessStatusCode)
                {
                    // Process the response data here
                    var responseData = await apiResponse.Content.ReadAsStringAsync();
                    // Perform data processing logic
                    // ...
                }
                else
                {
                    _logger.LogWarning("API request was not successful. Status code: {StatusCode}", apiResponse.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching and processing data.");
            }
        }
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();
                    services.AddHostedService<DataFetcherService>();
                });
    }
}
