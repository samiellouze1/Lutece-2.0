using StockService.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace StockService.Models
{
    public class Order:IEntityBase
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public OrderStatusEnum OrderStatus { get; set; }
        public Nullable<double> ExecutedPrice { get; set; }
        public Nullable<DateTime> DateExecution { get; set; }
        public virtual OriginalOrder OriginalOrder { get; set; }
    }
}
