using WorkplaceOps.Domain.Rules;

namespace WorkplaceOps.Application.Rules;

public sealed record RuleEvaluationResult(
    RuleApplicability Applicability,
    IReadOnlyList<RuleConditionEvaluationResult> Conditions
);

// This record will hold the information produced by evaluating an entire rule
