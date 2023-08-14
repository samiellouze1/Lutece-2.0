using StockService.Data.Enums;
using StockService.Models;
using System.ComponentModel.DataAnnotations;

namespace StockService.Data.DTOs
{
    public class OriginalOrderReadDTO
    {
        public string Id { get; set; }
        public OriginalOrderTypeEnum OriginalOrderType { get; set; }
        public double Price { get; set; }
        public int OriginalQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public DateTime DateDeposit { get; set; }
        public int UserId { get; set; }
        public string StockId { get; set; }
        public OriginalOrderStatusEnum OriginalOrderStatus { get; set; }
    }
}
