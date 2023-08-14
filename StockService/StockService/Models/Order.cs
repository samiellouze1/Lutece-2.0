using StockService.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockService.Models
{
    public class Order:IEntityBase
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string Id{ get; set; }
        [Required]
        public OrderStatusEnum OrderStatus { get; set; } = OrderStatusEnum.Active;
        public Nullable<double> ExecutedPrice { get; set; }
        public Nullable<DateTime> DateExecution { get; set; }
        public virtual OriginalOrder OriginalOrder { get; set; }
    }
}
