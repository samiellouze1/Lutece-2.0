using AutoMapper;
using ProbabilityService.Models;

namespace ProbabilityService.Data.Profiles
{
    public class ProbabilityDistributionProfile:Profile
    {
        public ProbabilityDistributionProfile()
        {
            CreateMap<ProbabilityDistributionUnit, GrpcProbabilityDistributionModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Stock.Id));
        }
    }
}
