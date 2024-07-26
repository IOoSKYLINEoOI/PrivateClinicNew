using System.ComponentModel.DataAnnotations;

namespace Clinic.Web.Contracts.Receptions;

public record ReceptionRequest(
    [Required] DateTime DateReceipt,
    DateTime? DateOfReturn,
    string? Description,
    [Required] Guid UserId,
    [Required] Guid DepartmentId,
    [Required] Guid EmployeeId);
