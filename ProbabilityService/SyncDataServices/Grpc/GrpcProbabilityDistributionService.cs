using AutoMapper;
using Grpc.Core;
using ProbabilityService.Repo.IRepo;

namespace ProbabilityService.SyncDataServices.Grpc
{
    public class GrpcProbabilityDistributionService : GrpcProbabilityDistribution.GrpcProbabilityDistributionBase
    {
        private readonly IProbabilityDistributionRepo _repo;
        private readonly IMapper _mapper;

        public GrpcProbabilityDistributionService(IProbabilityDistributionRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async override Task<ProbabilityDistributionResponse> GetAllProbabilityDistributions(GetAllRequest request, ServerCallContext context)
        {
            var response = new ProbabilityDistributionResponse();
            var pdus = await _repo.GetAllAsync(pdu=>pdu.Stock);
            foreach (var pdu in pdus)
            {
                response.Probabilitydistribution.Add(_mapper.Map<GrpcProbabilityDistributionModel>(pdu));
            }
            return response;
        }
    }
}
