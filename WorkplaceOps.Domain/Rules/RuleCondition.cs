using System.Text.Json.Serialization;

namespace WorkplaceOps.Domain.Rules;

public class RuleCondition
{
    public Guid Id { get; set; } // PK
    public Guid RuleId { get; set; } // FK for Rule
    public RuleConditionField Field { get; set; } // The field of the business or operational profile to evaluate
    public RuleConditionOperator Operator { get; set; } // The operator to use for the evaluation
    public string ComparisonValue { get; set; } = string.Empty; // The value to compare against. Ex: "100" for EmployeeCount, "Manufacturing" for Industry, etc.
    [JsonIgnore]
    public Rule? Rule { get; set; } // Navigation property to the associated Rule
}

