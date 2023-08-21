using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProbabilityService.Models
{
    public class ProbabilityDistributionUnit:IEntityBase
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public double Price { get; set; }
        [Range(0.0, 1.0, ErrorMessage = "The value must be between 0 and 1.")]
        public double Probability { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
