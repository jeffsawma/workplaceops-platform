using WorkplaceOps.Domain.Businesses;

namespace WorkplaceOps.Application.Businesses
{
    public interface IBusinessOperationalProfileRepository
    {
        Task AddAsync(BusinessOperationalProfile businessOperationalProfile);

        Task<BusinessOperationalProfile?> GetByBusinessIdAsync(Guid businessId); // Finding the profile that belongs to one specific business
    }
}
