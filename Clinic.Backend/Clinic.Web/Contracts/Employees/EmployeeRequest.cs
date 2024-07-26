using System.ComponentModel.DataAnnotations;

namespace Clinic.Web.Contracts.Employee;

public record EmployeeRequest(
    [Required] DateOnly HiringDate,
    DateOnly? DateOfDismissal,
    string? Description,
    [Required] Guid UserId);

