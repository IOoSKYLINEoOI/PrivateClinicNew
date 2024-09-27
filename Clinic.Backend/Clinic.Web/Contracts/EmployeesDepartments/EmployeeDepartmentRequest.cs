using System.ComponentModel.DataAnnotations;

namespace Clinic.Web.Contracts.EmployeesDepartments;

public record EmployeeDepartmentRequest(
     [Required] Guid EmployeeId,
     [Required] Guid DepartmentId,
     string? Description,
     [Required] int PositionId);

