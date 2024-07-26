namespace Clinic.Web.Contracts.Departments;

public record DepartmentResponse(
    Guid id,
    string name,
    string? description,
    Guid adressId);

