using WorkplaceOps.Domain.Businesses;

namespace WorkplaceOps.Application.Businesses;

public class BusinessService
{
    private readonly IBusinessRepository _businessRepository;

    public BusinessService(IBusinessRepository businessRepository)
    {
        _businessRepository = businessRepository;
    }
    public async Task<Business> CreateBusinessAsync(CreateBusinessRequest request)
    {
        var business = new Business // business entity is created based on the request data
        {
            Id = Guid.NewGuid(), // Generate a new unique identifier for the business
            LegalName = request.LegalName,
            OperatingName = request.OperatingName,
            QuebecEnterpriseNumber = request.QuebecEnterpriseNumber,
            EmployeeCount = request.EmployeeCount,
            CreatedAtUtc = DateTime.UtcNow // Set the creation time to the current UTC time
        };

        await _businessRepository.AddAsync(business);

        return business;
    }

    public async Task<List<Business>> GetAllBusinessesAsync()
    {
        return await _businessRepository.GetAllAsync();
    }

    public async Task<Business?> GetBusinessByIdAsync(Guid id)
    {
        return await _businessRepository.GetByIdAsync(id);
    }
}

// For now, the BusinessService class is responsible for handling business logic related to Business entities. It provides methods to create a new business and retrieve all businesses. The CreateBusinessAsync method takes a CreateBusinessRequest object, creates a new Business entity, and saves it using the IBusinessRepository. The GetAllBusinessesAsync method retrieves all businesses from the repository
