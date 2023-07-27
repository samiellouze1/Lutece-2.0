using StockService.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace StockService.Models
{
    public class Order
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int DepositorId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public TypeEnum Type { get; set; }
        [Required]
        public DateTime DepositDate { get; set; }
    }
}
