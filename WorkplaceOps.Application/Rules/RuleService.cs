using WorkplaceOps.Domain.Rules;

namespace WorkplaceOps.Application.Rules;

public class RuleService
{
    private readonly IRuleRepository _ruleRepository;

    public RuleService(IRuleRepository ruleRepository)
    {
        _ruleRepository = ruleRepository;
    }

    public async Task<Rule> CreateRuleAsync(CreateRuleRequest request)
    {
        var rule = new Rule
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Version = 1,
            IsActive = request.IsActive,
            CreatedAtUtc = DateTime.UtcNow
        };

        await _ruleRepository.AddAsync(rule);

        return rule;
    }

    public async Task<List<Rule>> GetAllRulesAsync()
    {
        return await _ruleRepository.GetAllAsync();
    }

    public async Task<Rule?> GetRuleByIdAsync(Guid id)
    {
        return await _ruleRepository.GetByIdAsync(id);
    }

    public async Task<RuleCondition> AddConditionAsync(
        Guid ruleId,
        CreateRuleConditionRequest request)
    {
        var rule = await _ruleRepository.GetByIdAsync(ruleId);

        if (rule is null)
        {
            throw new KeyNotFoundException("Rule not found.");
        }

        if (request.Field is null || request.Operator is null)
        {
            throw new ArgumentException("Field and operator are required.");
        }

        var condition = new RuleCondition
        {
            Id = Guid.NewGuid(),
            RuleId = ruleId,
            Field = request.Field.Value,
            Operator = request.Operator.Value,
            ComparisonValue = request.ComparisonValue
        };

        await _ruleRepository.AddConditionAsync(condition);

        return condition;
    }
}


