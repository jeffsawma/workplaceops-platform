using Microsoft.EntityFrameworkCore;
using WorkplaceOps.Application.Businesses;
using WorkplaceOps.Domain.Businesses;
using WorkplaceOps.Infrastructure.Persistence;

namespace WorkplaceOps.Infrastructure.Businesses;
public class BusinessOperationalProfileRepository : IBusinessOperationalProfileRepository
{
    private readonly AppDbContext _context;

    public BusinessOperationalProfileRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(BusinessOperationalProfile businessOperationalProfile)
    {
        await _context.BusinessOperationalProfiles.AddAsync(businessOperationalProfile);
        await _context.SaveChangesAsync();
    }

    public async Task<BusinessOperationalProfile?> GetByBusinessIdAsync(Guid businessId)
    {
        return await _context.BusinessOperationalProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(businessOperationalProfile => businessOperationalProfile.BusinessId == businessId);
    }
}

