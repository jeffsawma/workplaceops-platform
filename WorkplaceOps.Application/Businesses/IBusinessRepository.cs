using WorkplaceOps.Domain.Businesses;

namespace WorkplaceOps.Application.Businesses
{
    public interface IBusinessRepository
    {
        Task AddAsync(Business business); // This method is intended to perform an asynchronous operation related to a Business entity. The specific operation is not defined in the interface and should be implemented by the classes that implement this interface

        Task<List<Business>> GetAllAsync(); // This method is intended to retrieve a list of all Business entities asynchronously. The specific implementation should be provided by the classes that implement this interface

        Task<Business?> GetByIdAsync(Guid id); // This method is intended to retrieve a Business entity by its unique identifier asynchronously. The specific implementation should be provided by the classes that implement this interface
    }
}

// Application defines the business logic and orchestrates the flow of data between the domain and infrastructure layers. It contains services, use cases, and interfaces that define the operations that can be performed on the domain entities. The IBusinessRepository interface is part of this layer, providing an abstraction for data access operations related to Business entities
