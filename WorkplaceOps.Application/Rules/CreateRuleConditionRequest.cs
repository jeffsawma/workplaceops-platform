using System.ComponentModel.DataAnnotations;
using WorkplaceOps.Domain.Rules;

namespace WorkplaceOps.Application.Rules;
public class CreateRuleConditionRequest
{
    [Required]
    public RuleConditionField? Field { get; set; }

    [Required]
    public RuleConditionOperator? Operator { get; set; }

    [Required]
    [MaxLength(200)]
    public string ComparisonValue { get; set; } = string.Empty;
}

