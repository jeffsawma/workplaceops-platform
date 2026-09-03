namespace WorkplaceOps.Domain.Businesses;

public class Business
{
    public Guid Id { get; set; }

    public string LegalName { get; set; } = string.Empty;

    public string? OperatingName { get; set; }

    public string? QuebecEnterpriseNumber { get; set; }

    public int EmployeeCount { get; set; }

    public DateTime CreatedAtUtc { get; set; }
}
