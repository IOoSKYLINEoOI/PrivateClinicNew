namespace Clinic.Web.Contracts.Employee;

public record EmployeeResponse(
    Guid id,
    DateOnly HiringDate,
    DateOnly? DateOfDismissal,
    string? Description,
    Guid UserId);

