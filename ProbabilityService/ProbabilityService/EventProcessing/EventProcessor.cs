using AutoMapper;
using System.Diagnostics;

namespace ProbabilityService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopefactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopefactory = scopeFactory;
            _mapper = mapper;
        }
        public void ProcessEvent(string message)
        {
            throw new NotImplementedException();
        }
        private EventType DetermineEvent ( string notificationMessage)
        {
            throw new NotImplementedException ();
        }
        private void addStockPrice(string stockpricePublishedMessage)
        {
            throw new NotImplementedException () ;
        }
    }
    enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}
