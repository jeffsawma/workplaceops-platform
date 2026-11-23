using WorkplaceOps.Domain.Rules;
using WorkplaceOps.Application.Businesses;


namespace WorkplaceOps.Application.Rules;

public class RuleService
{
    private readonly IRuleRepository _ruleRepository;
    private readonly IBusinessRepository _businessRepository;
    private readonly IBusinessOperationalProfileRepository _profileRepository;
    private readonly RuleEvaluator _ruleEvaluator;

    public RuleService(
        IRuleRepository ruleRepository,
        IBusinessRepository businessRepository,
        IBusinessOperationalProfileRepository profileRepository,
        RuleEvaluator ruleEvaluator)
    {
        _ruleRepository = ruleRepository;
        _businessRepository = businessRepository;
        _profileRepository = profileRepository;
        _ruleEvaluator = ruleEvaluator;
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

    public async Task<RuleApplicability> EvaluateRuleForBusinessAsync(
        Guid ruleId,
        Guid businessId)
    {
        var rule = await _ruleRepository.GetByIdAsync(ruleId);

        if (rule is null)
        {
            throw new KeyNotFoundException("Rule not found.");
        }

        var business = await _businessRepository.GetByIdAsync(businessId);

        if (business is null)
        {
            throw new KeyNotFoundException("Business not found.");
        }

        var profile = await _profileRepository.GetByBusinessIdAsync(businessId);

        return _ruleEvaluator.EvaluateApplicability(rule, business, profile);

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


