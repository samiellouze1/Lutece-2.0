using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecommandationService.Models
{
    public class Stock
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string StockId { get; set; }
        public virtual BuyingOriginalOrderStock BuyingOriginalOrderStock { get; set; }
        public virtual List<ProbabilityDistributionUnit> ProbabilityDistributionUnits { get; set;}
    }
}
