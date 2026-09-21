namespace WorkplaceOps.Domain.Businesses;

public class Business
{
    public Guid Id { get; set; } // PK // Unique identifier for the business entity
    public string LegalName { get; set; } = string.Empty; // Required property, cannot be null or empty
    public string? OperatingName { get; set; } // Optional property, can be null
    public string? QuebecEnterpriseNumber { get; set; } // Optional property, can be null
    public int EmployeeCount { get; set; } // Required property, cannot be null
    public DateTime CreatedAtUtc { get; set; } // Required property, cannot be null, represents the creation time in UTC

    // Navigation relationships
    public BusinessOperationalProfile? OperationalProfile { get; set; } // one-to-one relationship
}

// Business: identity/basic facts


