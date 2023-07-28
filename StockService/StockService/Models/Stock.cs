using System.ComponentModel.DataAnnotations;

namespace StockService.Models
{
    public class Stock
    {
        [Key] 
        [Required]
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; }
        public virtual List<StockUser> StockUsers { get; set; } = new List<StockUser>();

    }
}
