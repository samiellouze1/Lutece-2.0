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
        public virtual List<StockUnit> StockUnits { get; set; } = new List<StockUnit>();
        public virtual List<OriginalOrder> OriginalOrders { get; set; } = new List<OriginalOrder>();
    }
}
