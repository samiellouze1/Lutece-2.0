using StockService.Data.Enums;
using StockService.Models;
using System.ComponentModel.DataAnnotations;

namespace StockService.DTOs
{
    public class UserCreateDTO
    {
        public UserTypeEnum UserType { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }
        public virtual List<StockUnit> StockUnits { get; set; } = new List<StockUnit>();
        public virtual List<OriginalOrder> OriginalOrders { get; set; } = new List<OriginalOrder>();
    }
}
