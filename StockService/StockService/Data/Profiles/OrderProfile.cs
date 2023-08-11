using AutoMapper;
using StockService.Data.DTOs;
using StockService.Models;

namespace StockService.Data.Profiles
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
