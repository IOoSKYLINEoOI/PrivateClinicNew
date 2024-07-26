using CSharpFunctionalExtensions;

namespace Clinic.Core.Models;

public class EmployeeDepartment
{
    public const int MaxDescriptionEmployeeDepartmentLength = 250;

    private EmployeeDepartment(Guid employeeId, Guid departmentId, string? description, Guid positionId)
    {
        EmployeeId = employeeId;
        DepartmentId = departmentId;
        Description = description;
        PositionId = positionId;
    }

    public Guid EmployeeId { get; set; }
    public Guid DepartmentId { get; set; }
    public string? Description { get; set; }
    public Guid PositionId { get; set; }

    public static Result<EmployeeDepartment> Create(Guid employeeId, Guid departmentId, string? description, Guid positionId)
    {
        if (!string.IsNullOrEmpty(description) && description.Length > MaxDescriptionEmployeeDepartmentLength)
        {
            return Result.Failure<EmployeeDepartment>($"'{nameof(description)}' cannot be more than {MaxDescriptionEmployeeDepartmentLength} characters.");
        }

        var employeeDepartment = new EmployeeDepartment(employeeId, departmentId, description, positionId);

        return Result.Success(employeeDepartment);
    }
}