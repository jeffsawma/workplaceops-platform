namespace WorkplaceOps.Domain.Rules;
public class Rule
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Version { get; set; } = 1;
    public bool IsActive { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}

