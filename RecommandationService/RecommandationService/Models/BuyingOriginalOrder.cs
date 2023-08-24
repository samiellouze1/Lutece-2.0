using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace RecommandationService.Models
{
    public class BuyingOriginalOrder
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int OriginalQuantity { get; set; }
        public virtual List<BuyingOriginalOrderStock> BuyingOriginalOrderStocks { get; set; }
        public virtual RecommandationResponse RecommandationResponse { get; set; }
    }
}
