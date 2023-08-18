using System.ComponentModel.DataAnnotations;

namespace ProbabilityService.Models
{
    public class Stock:IEntityBase
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string StockId { get; set; }
        public virtual List<StockTrace> StockTraces { get; set; }
        public virtual List<ProbabilityDistributionUnit> ProbabilityDistributionUnits {get;set;}
    }
}
