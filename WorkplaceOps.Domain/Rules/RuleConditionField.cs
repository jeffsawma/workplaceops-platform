namespace WorkplaceOps.Domain.Rules
{
    public enum RuleConditionField
    {
        EmployeeCount,
        Industry,
        LocationCount,
        HasRemoteEmployees,
        HasUnionizedEmployees
    }
}
// This enum represents the fields that can be used in rule conditions. Each value corresponds to a property in the Business or BusinessOperationalProfile entities that can be evaluated when applying rules
