namespace WorkplaceOps.Domain.Businesses;

public class BusinessOperationalProfile
{
    public Guid Id { get; set; } // PK 
    public Guid BusinessId { get; set; } // FK for Business
    public string? Industry { get; set; }
    public int LocationCount { get; set; }
    public bool HasRemoteEmployees { get; set; }
    public bool HasUnionizedEmployees { get; set; }
    public DateTime CreatedAtUtc { get; set; }

    // Navigation relationships
    public Business? Business { get; set; } // one-to-one relationship
}

// BusinessOperationalProfile: operational facts that future rules can evaluate

