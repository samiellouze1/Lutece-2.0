using StockService.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace StockService.Models
{
    public class User:IEntityBase
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public UserTypeEnum UserType { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public double Balance { get; set; }
        public virtual List<StockUnit> StockUnits { get; set; } = new List<StockUnit>();
        public virtual List<OriginalOrder> OriginalOrders { get; set; } = new List<OriginalOrder>();
    }
}
