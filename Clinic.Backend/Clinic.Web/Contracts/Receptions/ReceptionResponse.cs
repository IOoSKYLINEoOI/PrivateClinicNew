namespace Clinic.Web.Contracts.Receptions;

public record ReceptionResponse(
    Guid Id,
    DateTime DateReceipt,
    DateTime? DateOfReturn,
    string? Description,
    Guid UserId,
    Guid DepartmentId,
    Guid EmployeeId);

