using StockService.Data.Enums;
using StockService.Models;

namespace StockService.DTOs
{
    public class UserReadDTO
    {
        public string Id{ get; set; }
        public UserTypeEnum UserType { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }
    }
}
