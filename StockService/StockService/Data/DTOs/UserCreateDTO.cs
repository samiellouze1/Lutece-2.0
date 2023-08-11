using StockService.Data.Enums;
using StockService.Models;
using System.ComponentModel.DataAnnotations;

namespace StockService.Data.DTOs
{
    public class UserCreateDTO
    {
        public UserTypeEnum UserType { get; set; }
        public string UserName { get; set; }
        public double Balance { get; set; }
    }
}
