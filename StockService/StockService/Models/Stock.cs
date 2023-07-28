using System.ComponentModel.DataAnnotations;

namespace StockService.Models
{
    public class Stock:IEntityBase
    {
        [Key] 
        [Required]
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; }
        [Required]
        public double AveragePrice { get; set; }
        public virtual List<StockUnit> StockUnits { get; set; } = new List<StockUnit>();
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}
