using System.ComponentModel.DataAnnotations;

namespace ProbabilityService.Models
{
    public class StockTrace
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime dateTime { get; set; }= DateTime.Now;
        [Required]
        public virtual Stock Stock { get; set; }
    }
}
