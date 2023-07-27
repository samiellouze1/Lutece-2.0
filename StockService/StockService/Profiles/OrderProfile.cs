using AutoMapper;
using StockService.DTOs;
using StockService.Models;

namespace StockService.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            //Source >> target
            CreateMap<Order, OrderReadDTO>();
            CreateMap<OrderCreateDTO, Order>();
        }
    }
}
