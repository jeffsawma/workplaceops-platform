using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using WorkplaceOps.Domain.Businesses;
using WorkplaceOps.Domain.Rules;

namespace WorkplaceOps.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // DbSet for the Business, BusinessOperationalProfile and Rule entities
    public DbSet<Business> Businesses => Set<Business>(); // This property represents the collection of Business entities in the database
    public DbSet<BusinessOperationalProfile> BusinessOperationalProfiles => Set<BusinessOperationalProfile>();
    public DbSet<Rule> Rules => Set<Rule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Business>()
            .HasOne(business => business.OperationalProfile)
            .WithOne(profile => profile.Business)
            .HasForeignKey<BusinessOperationalProfile>(
                profile => profile.BusinessId);
    }
}
