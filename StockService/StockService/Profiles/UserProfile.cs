using AutoMapper;
using StockService.DTOs;
using StockService.Models;

namespace StockService.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //Source >> target
            CreateMap<User, UserReadDTO>();
            CreateMap<UserCreateDTO, User>();
        }
    }
}
