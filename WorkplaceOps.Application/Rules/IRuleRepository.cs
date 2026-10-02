using WorkplaceOps.Domain.Rules;

namespace WorkplaceOps.Application.Rules;

public interface IRuleRepository
{
    Task AddAsync(Rule rule);
    Task<List<Rule>> GetAllAsync();
    Task<Rule?> GetByIdAsync(Guid id);
}
