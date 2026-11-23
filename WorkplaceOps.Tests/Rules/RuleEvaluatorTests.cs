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


    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public void Evaluate_ReturnsExpectedResult_ForRemoteEmployees(
        bool hasRemoteEmployees,
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
                    Field = RuleConditionField.HasRemoteEmployees,
                    Operator = RuleConditionOperator.Equal,
                    ComparisonValue = "true"
                }
            ]
        };

        var business = new Business();

        var profile = new BusinessOperationalProfile
        {
            HasRemoteEmployees = hasRemoteEmployees
        };

        var result = evaluator.Evaluate(rule, business, profile);

        Assert.Equal(expectedResult, result);
    }


    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public void Evaluate_ReturnsExpectedResult_ForUnionizedEmployees(
        bool hasUnionizedEmployees,
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
                    Field = RuleConditionField.HasUnionizedEmployees,
                    Operator = RuleConditionOperator.NotEqual,
                    ComparisonValue = "true"
                }
            ]
        };

        var business = new Business();

        var profile = new BusinessOperationalProfile
        {
            HasUnionizedEmployees = hasUnionizedEmployees
        };

        var result = evaluator.Evaluate(rule, business, profile);

        Assert.Equal(expectedResult, result);
    }


    [Theory]
    [InlineData("Retail", true)]
    [InlineData("retail", true)]
    [InlineData("Manufacturing", false)]
    public void Evaluate_ReturnsExpectedResult_ForIndustry(
        string industry,
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
                    Field = RuleConditionField.Industry,
                    Operator = RuleConditionOperator.Equal,
                    ComparisonValue = "Retail"
                }
            ]
        };

        var business = new Business();

        var profile = new BusinessOperationalProfile
        {
            Industry = industry
        };

        var result = evaluator.Evaluate(rule, business, profile);

        Assert.Equal(expectedResult, result);
    }


    [Theory]
    [InlineData(25, "Retail", true, true)]
    [InlineData(15, "Retail", true, false)]
    [InlineData(25, "Manufacturing", true, false)]
    [InlineData(25, "Retail", false, false)]
    public void Evaluate_ReturnsExpectedResult_ForMultipleConditions(
        int employeeCount,
        string industry,
        bool hasRemoteEmployees,
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
                    Field = RuleConditionField.EmployeeCount,
                    Operator = RuleConditionOperator.GreaterThanOrEqual,
                    ComparisonValue = "20"
                },

                new RuleCondition
                {
                    Field = RuleConditionField.Industry,
                    Operator = RuleConditionOperator.Equal,
                    ComparisonValue = "Retail"
                },

                new RuleCondition
                {
                    Field = RuleConditionField.HasRemoteEmployees,
                    Operator = RuleConditionOperator.Equal,
                    ComparisonValue = "true"
                }
            ]
        };

        var business = new Business
        {
            EmployeeCount = employeeCount
        };

        var profile = new BusinessOperationalProfile
        {
            Industry = industry,
            HasRemoteEmployees = hasRemoteEmployees
        };

        var result = evaluator.Evaluate(rule, business, profile);

        Assert.Equal(expectedResult, result);
    }


    // Additional tests can be added here to cover more scenarios, such as testing inactive rules, rules with no conditions, and edge cases for each condition type
    // Testing an inactive rule
    [Fact]
    public void Evaluate_ReturnsFalse_WhenRuleIsInactive()
    {
        var evaluator = new RuleEvaluator();

        var rule = new Rule
        {
            IsActive = false, // Rule is inactive
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
            EmployeeCount = 25
        };

        var result = evaluator.Evaluate(rule, business, null);

        Assert.False(result); // Expecting false because the rule is inactive
    }


    // Testing a rule with no conditions
    [Fact]
    public void Evaluate_ReturnsFalse_WhenRuleHasNoConditions()
    {
        var evaluator = new RuleEvaluator();

        var rule = new Rule
        {
            IsActive = true,
            Conditions = [] // No conditions in the rule
        };

        var business = new Business
        {
            EmployeeCount = 25
        };

        var result = evaluator.Evaluate(rule, business, null);

        Assert.False(result);
    }


    // Testing a rule with a condition that requires a profile, but the profile is missing
    [Theory]
    [InlineData(RuleConditionField.LocationCount, "2")]
    [InlineData(RuleConditionField.Industry, "Retail")]
    [InlineData(RuleConditionField.HasRemoteEmployees, "true")]
    [InlineData(RuleConditionField.HasUnionizedEmployees, "true")]
    public void Evaluate_ReturnsFalse_WhenProfileIsMissing(
        RuleConditionField field,
        string comparisonValue)
    {
        var evaluator = new RuleEvaluator();

        var rule = new Rule
        {
            IsActive = true,
            Conditions =
            [
                new RuleCondition
                {
                    Field = field,
                    Operator = RuleConditionOperator.Equal,
                    ComparisonValue = comparisonValue
                }
            ]
        };

        var business = new Business();

        var result = evaluator.Evaluate(rule, business, null); // We are deliberately passing null for the profile 

        Assert.False(result);
    }


    // Testing a rule with a condition that has an invalid comparison value
    [Theory]
    [InlineData(RuleConditionField.EmployeeCount, "twenty")]
    [InlineData(RuleConditionField.HasRemoteEmployees, "yes")]
    [InlineData(RuleConditionField.Industry, " ")]
    public void Evaluate_ReturnsFalse_WhenComparisonValueIsInvalid(
        RuleConditionField field,
        string comparisonValue)
    {
        var evaluator = new RuleEvaluator();

        var rule = new Rule
        {
            IsActive = true,
            Conditions =
            [
                new RuleCondition
                {
                    Field = field,
                    Operator = RuleConditionOperator.Equal,
                    ComparisonValue = comparisonValue
                }
            ]
        };

        var business = new Business
        {
            EmployeeCount = 20
        };

        var profile = new BusinessOperationalProfile
        {
            HasRemoteEmployees = true,
            Industry = "Retail"
        };

        var result = evaluator.Evaluate(rule, business, profile);

        Assert.False(result);
    }


    // Testing a rule with unsupported operator for specific fields
    [Theory]
    [InlineData(RuleConditionField.HasRemoteEmployees, RuleConditionOperator.GreaterThan, "true")]
    [InlineData(RuleConditionField.Industry, RuleConditionOperator.LessThan, "Retail")]
    public void Evaluate_ReturnsFalse_WhenOperatorIsUnsupported(
        RuleConditionField field,
        RuleConditionOperator conditionOperator,
        string comparisonValue)
    {
        var evaluator = new RuleEvaluator();

        var rule = new Rule
        {
            IsActive = true,
            Conditions =
            [
                new RuleCondition
                {
                    Field = field,
                    Operator = conditionOperator,
                    ComparisonValue = comparisonValue
                }
            ]
        };

        var business = new Business();

        var profile = new BusinessOperationalProfile
        {
            HasRemoteEmployees = true,
            Industry = "Retail"
        };

        var result = evaluator.Evaluate(rule, business, profile);

        Assert.False(result);
    }


    // Testing all six integer comparison operators for the EmployeeCount field to ensure they behave as expected
    [Theory]
    [InlineData(RuleConditionOperator.Equal, 20, true)]
    [InlineData(RuleConditionOperator.Equal, 19, false)]

    [InlineData(RuleConditionOperator.NotEqual, 19, true)]
    [InlineData(RuleConditionOperator.NotEqual, 20, false)]

    [InlineData(RuleConditionOperator.GreaterThan, 21, true)]
    [InlineData(RuleConditionOperator.GreaterThan, 20, false)]

    [InlineData(RuleConditionOperator.GreaterThanOrEqual, 20, true)]
    [InlineData(RuleConditionOperator.GreaterThanOrEqual, 19, false)]

    [InlineData(RuleConditionOperator.LessThan, 19, true)]
    [InlineData(RuleConditionOperator.LessThan, 20, false)]

    [InlineData(RuleConditionOperator.LessThanOrEqual, 20, true)]
    [InlineData(RuleConditionOperator.LessThanOrEqual, 21, false)]
    public void Evaluate_ReturnsExpectedResult_ForIntegerOperators(
    RuleConditionOperator conditionOperator,
    int employeeCount,
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
                Field = RuleConditionField.EmployeeCount,
                Operator = conditionOperator,
                ComparisonValue = "20"
            }
            ]
        };

        var business = new Business
        {
            EmployeeCount = employeeCount
        };

        var result = evaluator.Evaluate(rule, business, null);

        Assert.Equal(expectedResult, result);
    }


    [Theory]
    [InlineData(25, RuleApplicability.Applicable)]
    [InlineData(15, RuleApplicability.NotApplicable)]
    public void EvaluateApplicability_ReturnsExpectedResult(
        int employeeCount,
        RuleApplicability expectedResult)
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
            EmployeeCount = employeeCount
        };

        var result = evaluator.EvaluateApplicability(rule, business, null);

        Assert.Equal(expectedResult, result);
    }
}
