using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProbabilityService.Models
{
    public class Stock:IEntityBase
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string StockId { get; set; }
        [Required]
        public double AveragePrice { get; set; }
        public virtual List<StockTrace> StockTraces { get; set; }
        public virtual List<ProbabilityDistributionUnit> ProbabilityDistributionUnits {get;set;}
    }
}
