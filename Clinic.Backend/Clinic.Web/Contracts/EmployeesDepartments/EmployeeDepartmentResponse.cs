namespace Clinic.Web.Contracts.EmployeesDepartments;

public record EmployeeDepartmentResponse(
        Guid EmployeeId,
        Guid DepartmentId,
        string? Description,
        Guid PositionId);

