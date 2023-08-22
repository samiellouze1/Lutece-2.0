using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecommandationService.Models
{
    public class RecommandationResponse
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public virtual List<BuyingOriginalOrder> BuyingOriginalOrders { get; set; }
        public virtual RecommandationRequest RecommandationRequest { get; set; }
    }
}
