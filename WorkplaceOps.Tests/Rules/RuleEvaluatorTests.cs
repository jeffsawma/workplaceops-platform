using Microsoft.VisualBasic;
using WorkplaceOps.Application.Rules;
using WorkplaceOps.Domain.Businesses;
using WorkplaceOps.Domain.Rules;

namespace WorkplaceOps.Tests.Rules;

public class RuleEvaluatorTests
{
    [Fact]
    public void Evaluate_ReturnsTrue_WhenEmployeeCountMeetsCondition()
    {
        var evaluator = new RuleEvaluator();

        // Creating a rule that checks if the employee count is greater than or equal to 20
        var rule = new Rule
        {
            IsActive = true,
            Conditions =
            [
                new RuleCondition
                {
                    Field = RuleConditionField.EmployeeCount,
                    Operator = RuleConditionOperator.GreaterThanOrEqual,
                    ComparisonValue = "20"
                }
            ]
        };

        // Creating a pretend business with an employee count of 25, which should satisfy the condition
        var business = new Business
        {
            EmployeeCount = 25
        };

        // Evaluating the rule against the business
        var result = evaluator.Evaluate(rule, business, null);

        // Asserting that the evaluation result is true, meaning the business satisfies the rule condition
        Assert.True(result);
    }

    [Fact]
    public void Evaluate_ReturnsFalse_WhenEmployeeCountBelowThreshold()
    {
        var evaluator = new RuleEvaluator();

        var rule = new Rule
        {
            IsActive = true,
            Conditions =
            [
                new RuleCondition
                {
                    Field = RuleConditionField.EmployeeCount,
                    Operator = RuleConditionOperator.GreaterThanOrEqual,
                    ComparisonValue = "20"
                }
            ]
        };

        var business = new Business
        {
            EmployeeCount = 15
        };

        var result = evaluator.Evaluate(rule, business, null);

        Assert.False(result);
    }

    [Theory]
    [InlineData(3, true)]
    [InlineData(1, false)]
    public void Evaluate_ReturnsExpectedResult_ForLocationCount(
        int locationCount,
        bool expectedResult)
    {
        var evaluator = new RuleEvaluator();

        var rule = new Rule
        {
            IsActive = true,
            Conditions =
            [
                new RuleCondition
                {
                    Field = RuleConditionField.LocationCount,
                    Operator = RuleConditionOperator.GreaterThanOrEqual,
                    ComparisonValue = "2"
                }
            ]
        };

        var business = new Business();

        var profile = new BusinessOperationalProfile
        {
            LocationCount = locationCount
        };

        var result = evaluator.Evaluate(rule, business, profile);

        Assert.Equal(expectedResult, result);
    }
}
