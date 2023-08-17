using StockService.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockService.Models
{
    public class StockUnit : IEntityBase
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string Id{ get; set; }
        public virtual User User { get; set; }
        [Required]
        public double PriceBought { get; set; }
        [Required]
        public DateTime DateBought { get; set; }
        [Required]
        public StockUnitStatusEnum StockUnitStatus { get; set; } = StockUnitStatusEnum.InStock;
        public virtual Stock Stock { get; set; }
    }
}
