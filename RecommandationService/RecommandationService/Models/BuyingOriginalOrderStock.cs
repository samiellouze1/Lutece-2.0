using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecommandationService.Models
{
    public class BuyingOriginalOrderStock
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual BuyingOriginalOrder BuyingOriginalOrder { get; set; }
    }
}
