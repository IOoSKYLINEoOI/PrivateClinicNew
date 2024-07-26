using System.ComponentModel.DataAnnotations;

namespace Clinic.Web.Contracts.Departments;

public record DepartmentRequest(
    [Required] string Name,
    string? Description,
    [Required] Guid AdressId);
