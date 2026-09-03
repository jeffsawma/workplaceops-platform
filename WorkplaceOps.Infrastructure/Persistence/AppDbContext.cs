using Microsoft.EntityFrameworkCore;
using WorkplaceOps.Domain.Businesses;

namespace WorkplaceOps.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // DbSet for the Business entity
    public DbSet<Business> Businesses => Set<Business>(); // This property represents the collection of Business entities in the database
}
