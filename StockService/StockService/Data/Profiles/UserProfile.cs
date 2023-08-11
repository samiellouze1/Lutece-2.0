using AutoMapper;
using StockService.Data.DTOs;
using StockService.Models;

namespace StockService.Data.Profiles
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
