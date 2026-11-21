using WorkplaceOps.Domain.Businesses;
using WorkplaceOps.Domain.Rules;

namespace WorkplaceOps.Application.Rules;

public class RuleEvaluator
{
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

            _ => false
        };
    }

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
}

// Contains the actual rule-evaluation logic
