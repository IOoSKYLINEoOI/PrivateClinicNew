using CSharpFunctionalExtensions;

namespace Clinic.Core.Models;

public class Employee
{
    public const int MaxDescriptionEmployeeLength = 250;
    public static DateOnly DateNow = DateOnly.FromDateTime(DateTime.Now);

    private Employee(Guid id, DateOnly hiringDate, DateOnly? dateOfDismissal, string? description, Guid userId)
    {
        Id = id;
        HiringDate = hiringDate;
        DateOfDismissal = dateOfDismissal;
        Description = description;
        UserId = userId;
    }

    public Guid Id { get; set; }
    public DateOnly HiringDate { get; set; }
    public DateOnly? DateOfDismissal { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }

    public static Result<Employee> Create(Guid id, DateOnly hiringDate, DateOnly? dateOfDismissal, string? description, Guid userId)
    {
        if (hiringDate.CompareTo(DateNow) > 0)
        {
            return Result.Failure<Employee>($"'{nameof(hiringDate)}' cannot be in the future.");
        }
        if (!string.IsNullOrEmpty(description) && description.Length > MaxDescriptionEmployeeLength)
        {
            return Result.Failure<Employee>($"'{nameof(description)}' cannot be more than {MaxDescriptionEmployeeLength} characters.");
        }

        var employee = new Employee(id, hiringDate, dateOfDismissal, description, userId);

        return Result.Success(employee);
    }
}