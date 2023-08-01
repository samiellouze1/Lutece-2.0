using System.ComponentModel.DataAnnotations;

namespace StockService.Models
{
    public class Stock:IEntityBase
    {
        [Key] 
        [Required]
        public string Id{ get; set; }
        [Required] 
        public string Name { get; set; }
        [Required]
        public double AveragePrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        public virtual List<StockUnit> StockUnits { get; set; } = new List<StockUnit>();
        public virtual List<OriginalOrder> OriginalOrders { get; set; } = new List<OriginalOrder>();
    }
}
