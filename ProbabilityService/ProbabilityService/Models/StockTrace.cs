using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProbabilityService.Models
{
    public class StockTrace : IEntityBase
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public double AveragePrice { get; set; }
        [Required]
        public DateTime dateTime { get; set; } = DateTime.Now;
        public virtual Stock Stock { get; set; }
    }
}
