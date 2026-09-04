using System.ComponentModel.DataAnnotations;

namespace WorkplaceOps.Application.Businesses
{
    public class CreateBusinessRequest
    {
        [Required] // LegalName is a required property, cannot be null or empty
        [MaxLength(200)]
        public string LegalName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? OperatingName { get; set; }

        [MaxLength(10)] // A Quebec Enterprise Number has a maximum length of 10 characters
        public string? QuebecEnterpriseNumber { get; set; }

        [Range(0, 1_000_000)] // Employee count must be a non-negative integer
        public int EmployeeCount { get; set; }
    }
}

// The CreateBusinessRequest class is a simple DTO (Data Transfer Object) that represents the data required to create a new Business entity
// Then we clean up and harden this feature by adding validation attributes to the CreateBusinessRequest class. This ensures that the data provided for creating a business meets certain criteria before it is processed further
