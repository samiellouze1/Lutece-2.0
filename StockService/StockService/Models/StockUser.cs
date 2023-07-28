using StockService.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace StockService.Models
{
    public class StockUser
    {
        public virtual User User { get; set; }
        public virtual Stock Stock { get;set; }
        [Required]
        public StockUserStatusEnum StockUserStatus { get; set; }
        [Required]
        public DateTime DateBought { get; set; }
        public virtual User User {get;set; }
    }
}
