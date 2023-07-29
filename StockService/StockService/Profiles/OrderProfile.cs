using AutoMapper;
using StockService.DTOs;
using StockService.Models;

namespace StockService.Profiles
{
    public class OriginalOrderProfile : Profile
    {
        public OriginalOrderProfile()
        {
            //Source >> target
            CreateMap<OriginalOrder, OriginalOrderReadDTO>();
            CreateMap<OriginalOrderCreateDTO, OriginalOrder>();
        }
    }
}
