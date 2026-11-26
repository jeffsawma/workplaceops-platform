using WorkplaceOps.Domain.Rules;

namespace WorkplaceOps.Application.Rules;

public sealed record RuleEvaluationResult(
    RuleApplicability Applicability,
    string Explanation, // Added to provide a human-readable explanation of the evaluation result
    IReadOnlyList<RuleConditionEvaluationResult> Conditions // Providing more detailed explanations for individual conditions 
);

// This record will hold the information produced by evaluating an entire rule
