using StockService.Data.Enums;
using StockService.Models;
using System.ComponentModel.DataAnnotations;

namespace StockService.Data.DTOs
{
    public class OriginalOrderCreateDTO
    {
        public OrderTypeEnum OrderType { get; set; }
        public double Price { get; set; }
        public int OriginalQuantity { get; set; }
        public string StockId { get; set; }
    }
}
