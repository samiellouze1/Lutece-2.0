using AutoMapper;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OrderSimulator.Models;

namespace OrderSimulator.SyncDataServices.Grpc
{
    public class StockDataClient:IStockDataClient
    {
        private readonly IConfiguration _config;
        private readonly IMapper mapper;

        public IEnumerable<Stock> ReturnAllStocks()
        {
            Console.WriteLine($"..... Calling Grpc service {_config["GrpcPlatform"]}");
            var channel = GrpcChannel.ForAddress(_config["GrpcStock"]);
            var client = new GrpcStock.GrpcStockClient(channel);
            var request = new GetAllRequest();
            try
            {
                var response = client.GetAllPlatforms(request);
                return _mapper.Map<IEnumerable<Stock>>(response.Stock);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"could not call grpc server {ex.Message}");
                return null;
            }
        }
    }

}
