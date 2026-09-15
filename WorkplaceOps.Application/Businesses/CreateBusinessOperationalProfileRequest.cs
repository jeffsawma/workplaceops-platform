using System.ComponentModel.DataAnnotations;

namespace WorkplaceOps.Application.Businesses
{
    public class CreateBusinessOperationalProfileRequest
    {
        [Required]
        public Guid BusinessId { get; set; } // FK

        [MaxLength(200)]
        public string? Industry { get; set; }

        [Range(1, 10_000)] // Minimum one location per business
        public int LocationCount { get; set; }

        public bool HasRemoteEmployees { get; set; }

        public bool HasUnionizedEmployees { get; set; }
    }
}
