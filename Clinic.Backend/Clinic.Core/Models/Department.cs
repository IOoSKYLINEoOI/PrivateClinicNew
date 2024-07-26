using CSharpFunctionalExtensions;

namespace Clinic.Core.Models;

public class Department
{
    public const int MaxDepartmentLength = 60;
    public const int MaxDescriptionDepartmentLength = 250;

    private Department(Guid id, string name, string? description, Guid addressId)
    {
        Id = id;
        Name = name;
        Description = description;
        AddressId = addressId;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid AddressId { get; set; }

    public static Result<Department> Create(Guid id, string name, string? description, Guid addressId)
    {
        if (string.IsNullOrEmpty(name) || name.Length > MaxDepartmentLength)
        {
            return Result.Failure<Department>($"'{nameof(name)}' cannot be null, empty or more than {MaxDepartmentLength} characters.");
        }
        if (!string.IsNullOrEmpty(description) && description.Length > MaxDescriptionDepartmentLength)
        {
            return Result.Failure<Department>($"'{nameof(description)}' cannot be more than {MaxDescriptionDepartmentLength} characters.");
        }

        var department = new Department(id, name, description, addressId);

        return Result.Success(department);
    }
}