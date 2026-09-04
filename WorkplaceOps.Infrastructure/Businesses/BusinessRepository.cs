using Microsoft.EntityFrameworkCore;
using WorkplaceOps.Application.Businesses;
using WorkplaceOps.Domain.Businesses;
using WorkplaceOps.Infrastructure.Persistence;

namespace WorkplaceOps.Infrastructure.Businesses;

public class BusinessRepository : IBusinessRepository
{
    private readonly AppDbContext _context;

    public BusinessRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Business business)
    {
        await _context.Businesses.AddAsync(business);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Business>> GetAllAsync()
    {
        return await _context.Businesses
            .AsNoTracking() // Used to disable change tracking for better performance when only reading data
            .ToListAsync();
    }

    public async Task<Business?> GetByIdAsync(Guid id)
    {
        return await _context.Businesses
            .AsNoTracking()
            .FirstOrDefaultAsync(business => business.Id == id); // Used to find the first business with the specified ID or return null if not found
    }
}
