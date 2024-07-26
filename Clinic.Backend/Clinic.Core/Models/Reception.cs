using CSharpFunctionalExtensions;

namespace Clinic.Core.Models;

public class Reception
{
    public const int MaxDescriptionReceptionLength = 250;

    private Reception(
        Guid id,
        DateTime dateReceipt,
        DateTime? dateOfReturn,
        string? description,
        Guid userId,
        Guid departmentId,
        Guid employeeId)
    {
        Id = id;
        DateReceipt = dateReceipt;
        DateOfReturn = dateOfReturn;
        Description = description;
        UserId = userId;
        DepartmentId = departmentId;
        EmployeeId = employeeId;
    }

    public Guid Id { get; set; }
    public DateTime DateReceipt { get; set; }
    public DateTime? DateOfReturn { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid EmployeeId { get; set; }

    public static Result<Reception> Create(
        Guid id,
        DateTime dateReceipt,
        DateTime? dateOfReturn,
        string? description,
        Guid userId,
        Guid departmentId,
        Guid employeeId)
    {
        if (!string.IsNullOrEmpty(description) && description.Length > MaxDescriptionReceptionLength)
        {
            return Result.Failure<Reception>($"'{nameof(description)}' cannot be more than {MaxDescriptionReceptionLength} characters.");
        }

        var reception = new Reception(id, dateReceipt, dateOfReturn, description, userId, departmentId, employeeId);

        return Result.Success(reception);
    }
}