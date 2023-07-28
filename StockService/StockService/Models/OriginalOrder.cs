using StockService.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace StockService.Models
{
    public class OriginalOrder:IEntityBase
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public OrderTypeEnum OrderType { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int OriginalQuantity { get; set; }
        [Required]
        public int RemainingQuantity { get; set; }
        [Required]
        public DateTime DateDeposit { get; set; }
        public virtual List<Order> Orders { get; set; } = new List<Order>();
        public virtual User User { get; set; }

    }
}
