using WorkplaceOps.Domain.Businesses;

namespace WorkplaceOps.Application.Businesses;

public class BusinessOperationalProfileService
{
    private readonly IBusinessOperationalProfileRepository _businessOperationalProfileRepository;
    private readonly IBusinessRepository _businessRepository;

    public BusinessOperationalProfileService(IBusinessOperationalProfileRepository businessOperationalProfileRepository,
        IBusinessRepository businessRepository)
    {
        _businessOperationalProfileRepository = businessOperationalProfileRepository;

        _businessRepository = businessRepository;
    }

    public async Task<BusinessOperationalProfile> CreateBusinessOperationalProfileAsync(
        CreateBusinessOperationalProfileRequest request)
    {
        var business = await _businessRepository.GetByIdAsync(request.BusinessId);

        if (business is null)
        {
            throw new KeyNotFoundException(
                "The specified business does not exist.");
        }

        var existingProfile = await _businessOperationalProfileRepository.GetByBusinessIdAsync(request.BusinessId);

        if (existingProfile is not null)
        {
            throw new InvalidOperationException(
                "An operational profile already exists for this business.");
        }

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
