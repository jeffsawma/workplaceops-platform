using WorkplaceOps.Domain.Rules;

namespace WorkplaceOps.Application.Rules;

public sealed record RuleConditionEvaluationResult(
    RuleConditionField Field,
    RuleConditionOperator Operator,
    string? ActualValue,
    string ExpectedValue,
    bool IsSatisfied,
    string Explanation
);

// This record will hold the information produced by evaluating one condition
