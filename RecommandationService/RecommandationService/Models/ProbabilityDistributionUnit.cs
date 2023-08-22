using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecommandationService.Models
{
    public class ProbabilityDistributionUnit
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [Range(0.0, 1.0, ErrorMessage = "The value must be between 0 and 1.")]
        public double Probability { get; set; }
        [Required]
        public virtual Stock Stock { get; set; }
    }
}
