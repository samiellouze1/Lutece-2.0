using StockService.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace StockService.DTOs
{
    public class OrderReadDTO
    {
        public int Id { get; set; }
        public int DepositorId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public OrderTypeEnum Type { get; set; }
        public DateTime DepositDate { get; set; }
    }
}
