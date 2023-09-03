using AutoMapper;
using Grpc.Net.Client;
using ProbabilityService;
using RecommandationService.Models;

namespace RecommandationService.SyncDataServices
{
    public class ProbabilityDistributionDataClient : IProbabilityDistributionDataClient
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public ProbabilityDistributionDataClient(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }
        public IEnumerable<ProbabilityDistributionUnit> ReturnAllProbabilityDistributionUnits()
        {
            Console.WriteLine($"..... Calling Grpc service {_config["GrpcProbability"]}");
            var channel = GrpcChannel.ForAddress(_config["GrpcPlatform"]);
            var client = new GrpcProbabilityDistribution.GrpcProbabilityDistributionClient(channel);
            var request = new GetAllRequest();
            try
            {
                var reply = client.GetALlPlatforms(request);
                return _mapper.Map<IEnumerable<ProbabilityDistributionUnit>>(reply.Platform);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"could not call grpc server {ex.Message}");
                return null;
            }
        }
    }
}
