
using ProbabilityService.Data.DTO;
using ProbabilityService.Repo.IRepo;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace ProbabilityService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopefactory;

        public EventProcessor(IServiceScopeFactory scopeFactory)
        {
            _scopefactory = scopeFactory;
        }
        public void ProcessEvent(string message)
        {
            var eventtype = DetermineEvent(message);
            switch(eventtype)
            {
                case EventType.Stock_Published:
                    modifystockinfo(message); 
                    break;
                    ;
                default:
                    break;
            }
        }
        private EventType DetermineEvent ( string notificationMessage)
        {
            Console.WriteLine("----------------Determining event-----");
            var eventType = JsonSerializer.Deserialize<GenericEventDTO>(notificationMessage);

            switch (eventType.Event)
            {
                case "Stock_Published":
                    Console.WriteLine("-----Stock_Published Event detected");
                    return EventType.Stock_Published;
                default:
                    Console.WriteLine("----- event does not concern us");
                    return EventType.Undetermined;
            }
        }
        private async void modifystockinfo(string stockPublishedMessage)
        {
            using (var scope = _scopefactory.CreateScope()) 
            {
                var stockRepo = scope.ServiceProvider.GetRequiredService<IStockRepo>();
                var stockpublishdto = JsonSerializer.Deserialize<StockPublishDTO>(stockPublishedMessage);
                try
                {
                    var stocks = await stockRepo.GetAllAsync();
                    var thestock = stocks.Where(s => s.StockId == stockpublishdto.Id).ToList()[0];
                    thestock.AveragePrice = stockpublishdto.AveragePrice;
                    await stockRepo.SaveChangesAsync();
                    Console.WriteLine("done the modifying");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("couldnt do the modifying exception : "+ex.Message);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
    enum EventType
    {
        Stock_Published,
        Undetermined
    }
}
