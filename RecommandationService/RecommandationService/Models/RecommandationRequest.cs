using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecommandationService.Models
{
    public class RecommandationRequest
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public double Balance { get; set; }
        [Required]
        public double a { get; set; }
        [Required]
        public double b { get; set; }
        public virtual RecommandationResponse RecommandationResponse {get;set;}
    }
}
