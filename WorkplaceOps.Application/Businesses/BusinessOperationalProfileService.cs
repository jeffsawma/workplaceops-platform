using WorkplaceOps.Domain.Businesses;

namespace WorkplaceOps.Application.Businesses;

public class BusinessOperationalProfileService
{
    private readonly IBusinessOperationalProfileRepository _businessOperationalProfileRepository;

    public BusinessOperationalProfileService(IBusinessOperationalProfileRepository businessOperationalProfileRepository)
    {
        _businessOperationalProfileRepository = businessOperationalProfileRepository;
    }

    public async Task<BusinessOperationalProfile> CreateBusinessOperationalProfileAsync(
        CreateBusinessOperationalProfileRequest request)
    {
        var profile = new BusinessOperationalProfile
        {
            Id = Guid.NewGuid(),
            BusinessId = request.BusinessId,
            Industry = request.Industry,
            LocationCount = request.LocationCount,
            HasRemoteEmployees = request.HasRemoteEmployees,
            HasUnionizedEmployees = request.HasUnionizedEmployees,
            CreatedAtUtc = DateTime.UtcNow
        };

        await _businessOperationalProfileRepository.AddAsync(profile);

        return profile;
    }

    public async Task<BusinessOperationalProfile?> GetByBusinessIdAsync(
    Guid businessId)
    {
        return await _businessOperationalProfileRepository.GetByBusinessIdAsync(businessId);
    }
}
