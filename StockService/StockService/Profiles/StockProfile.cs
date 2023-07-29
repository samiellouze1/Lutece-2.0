using AutoMapper;
using StockService.DTOs;
using StockService.Models;

namespace StockService.Profiles
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            //Source >> target
            CreateMap<Stock, StockReadDTO>();
            CreateMap<StockCreateDTO, Stock>();
        }
    }
}
