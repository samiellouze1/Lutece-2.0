using StockService.Data.Enums;
using StockService.Models;
using System.ComponentModel.DataAnnotations;

namespace StockService.DTOs
{
    public class OriginalOrderReadDTO
    {
        public int Id { get; set; }
        public OrderTypeEnum OrderType { get; set; }
        public double Price { get; set; }
        public int OriginalQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public DateTime DateDeposit { get; set; }
        public virtual List<Order> Orders { get; set; } = new List<Order>();
        public virtual User User { get; set; }
    }
}
