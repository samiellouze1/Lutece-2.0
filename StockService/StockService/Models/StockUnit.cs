using StockService.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace StockService.Models
{
    public class StockUnit : IEntityBase
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Stock Stock { get;set; }
        [Required]
        public StockUnitStatusEnum StockUnitStatus { get; set; }
        [Required]
        public DateTime DateBought { get; set; }
    }
}
