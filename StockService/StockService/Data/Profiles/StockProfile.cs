using AutoMapper;
using StockService.Data.DTOs;
using StockService.Models;

namespace StockService.Data.Profiles
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
