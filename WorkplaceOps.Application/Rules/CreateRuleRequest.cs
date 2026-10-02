using System.ComponentModel.DataAnnotations;

namespace WorkplaceOps.Application.Rules
{
    public class CreateRuleRequest
    {
        [Required]
        [MaxLength(200)]
        public String Title { get; set; } = string.Empty;

        [MaxLength(250)]
        public String? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
