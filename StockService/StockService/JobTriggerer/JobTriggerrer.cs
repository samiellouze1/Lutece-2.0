namespace StockService.JobTriggerer
{
    public class JobTriggerrer
    {

        public static async Task TriggerJob(IConfiguration _configuration, HttpClient _httpClient)
        {
            string endpointUrl = $"{_configuration["SimulatorService"]}" + "Simulator/Recurring";
            Console.WriteLine(endpointUrl);
            HttpResponseMessage response = await _httpClient.GetAsync(endpointUrl);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("awakened the job recurring");
            }
            else
            {
                Console.WriteLine("Could not do api call " + response.StatusCode);
            }
        }
    }
}
