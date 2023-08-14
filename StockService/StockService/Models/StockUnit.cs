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
        public virtual Stock Stock { get;set; }
        [Required]
        public StockUnitStatusEnum StockUnitStatus { get; set; } = StockUnitStatusEnum.InStock;
        [Required]
        public DateTime DateBought { get; set; }
    }
}
