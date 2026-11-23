using WorkplaceOps.Domain.Businesses;
using WorkplaceOps.Domain.Rules;

namespace WorkplaceOps.Application.Rules;

public class RuleEvaluator
{

    // Evaluates the applicability of a rule to a given business and its operational profile
    public RuleApplicability EvaluateApplicability(
        Rule rule,
        Business business,
        BusinessOperationalProfile? profile)
    {
        bool isApplicable = Evaluate(rule, business, profile);

        return isApplicable
            ? RuleApplicability.Applicable
            : RuleApplicability.NotApplicable;
    }

    // Evaluates whether a rule is satisfied based on the business and its operational profile
    public bool Evaluate(
        Rule rule,
        Business business,
        BusinessOperationalProfile? profile)
    {
        if (!rule.IsActive || rule.Conditions.Count == 0)
        {
            return false; // If the rule is not active or has no conditions, it cannot be satisfied
        }

        foreach (var condition in rule.Conditions)
        {
            if (!EvaluateCondition(condition, business, profile)) // Evaluate each condition against the business and profile
            {
                return false; // If any condition is not satisfied, the rule is not satisfied
            }
        }

        return true;
    }

    // Evaluates a single rule condition against the business and its operational profile
    private bool EvaluateCondition(
        RuleCondition condition,
        Business business,
        BusinessOperationalProfile? profile)
    {
        return condition.Field switch
        {
            RuleConditionField.EmployeeCount =>
                EvaluateIntegerCondition(
                    business.EmployeeCount,
                    condition.Operator,
                    condition.ComparisonValue),

            RuleConditionField.LocationCount =>
                profile is not null &&
                EvaluateIntegerCondition(
                    profile.LocationCount,
                    condition.Operator,
                    condition.ComparisonValue),

            RuleConditionField.HasRemoteEmployees =>
                profile is not null &&
                EvaluateBooleanCondition(
                    profile.HasRemoteEmployees,
                    condition.Operator,
                    condition.ComparisonValue),

            RuleConditionField.HasUnionizedEmployees =>
                profile is not null &&
                EvaluateBooleanCondition(
                    profile.HasUnionizedEmployees,
                    condition.Operator,
                    condition.ComparisonValue),

            RuleConditionField.Industry =>
                profile is not null &&
                EvaluateStringCondition(
                    profile.Industry,
                    condition.Operator,
                    condition.ComparisonValue),
            _ => false
        };
    }

    // Evaluates integer conditions based on the specified operator and comparison value
    private bool EvaluateIntegerCondition(
        int actualValue,
        RuleConditionOperator conditionOperator,
        string comparisonValue)
    {
        if (!int.TryParse(comparisonValue, out var expectedValue))
        {
            return false;
        }

        return conditionOperator switch
        {
            RuleConditionOperator.Equal => actualValue == expectedValue,
            RuleConditionOperator.NotEqual => actualValue != expectedValue,
            RuleConditionOperator.GreaterThan => actualValue > expectedValue,
            RuleConditionOperator.GreaterThanOrEqual => actualValue >= expectedValue,
            RuleConditionOperator.LessThan => actualValue < expectedValue,
            RuleConditionOperator.LessThanOrEqual => actualValue <= expectedValue,
            _ => false
        };
    }

    // Evaluates boolean conditions based on the specified operator and comparison value
    private bool EvaluateBooleanCondition(
        bool actualValue,
        RuleConditionOperator conditionOperator,
        string comparisonValue)
    {
        if (!bool.TryParse(comparisonValue, out var expectedValue))
        {
            return false;
        }

        return conditionOperator switch
        {
            RuleConditionOperator.Equal => actualValue == expectedValue,
            RuleConditionOperator.NotEqual => actualValue != expectedValue,
            _ => false
        };
    }

    // Evaluates string conditions based on the specified operator and comparison value
    private bool EvaluateStringCondition(
        string? actualValue,
        RuleConditionOperator conditionOperator,
        string comparisonValue)
    {
        if (string.IsNullOrWhiteSpace(actualValue) ||
            string.IsNullOrWhiteSpace(comparisonValue))
        {
            return false;
        }

        bool matches = string.Equals(
            actualValue,
            comparisonValue,
            StringComparison.OrdinalIgnoreCase); // Tells C# to ignore uppercase and lowercase when comparing the two strings

        return conditionOperator switch
        {
            RuleConditionOperator.Equal => matches,
            RuleConditionOperator.NotEqual => !matches,
            _ => false
        };
    }
}

// Contains the actual rule-evaluation logic

