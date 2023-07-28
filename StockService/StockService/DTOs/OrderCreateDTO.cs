using StockService.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace StockService.DTOs
{
    public class OrderCreateDTO
    {
        [Required]
        public int DepositorId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public OrderTypeEnum Type { get; set; }
        [Required]
        public DateTime DepositDate { get; set; }
    }
}
