using Microsoft.EntityFrameworkCore;
using WorkplaceOps.Application.Rules;
using WorkplaceOps.Domain.Rules;
using WorkplaceOps.Infrastructure.Persistence;

namespace WorkplaceOps.Infrastructure.Rules;

public class RuleRepository : IRuleRepository
{
    private readonly AppDbContext _context;

    public RuleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Rule rule)
    {
        await _context.Rules.AddAsync(rule);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Rule>> GetAllAsync()
    {
        return await _context.Rules
            .AsNoTracking()
            .OrderByDescending(rule => rule.CreatedAtUtc)
            .ToListAsync();
    }

    public async Task<Rule?> GetByIdAsync(Guid id)
    {
        return await _context.Rules
            .AsNoTracking()
            .FirstOrDefaultAsync(rule => rule.Id == id);
    }
}
