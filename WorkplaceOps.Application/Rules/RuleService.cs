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
}

