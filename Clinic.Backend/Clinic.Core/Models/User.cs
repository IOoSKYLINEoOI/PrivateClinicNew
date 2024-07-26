using CSharpFunctionalExtensions;

namespace Clinic.Core.Models;

public class User
{
    public const int MaxLength = 60;
    public const int MaxDescriptionLength = 250;
    public static readonly DateOnly MaxDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-18));
    public static readonly DateOnly MinDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-150));

    private User(
        Guid id,
        string firstName,
        string lastName,
        string? fatherName,
        string phoneNumber,
        DateOnly dateOfBirth,
        Guid? imageId,
        string email,
        string? description,
        string passwordHash)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        FatherName = fatherName;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
        ImageId = imageId;
        Email = email;
        Description = description;
        PasswordHash = passwordHash;
    }

    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string? FatherName { get; }
    public string PhoneNumber { get; }
    public DateOnly DateOfBirth { get; }
    public Guid? ImageId { get; }
    public string Email { get; }
    public string? Description { get; }
    public string PasswordHash { get; }

    public static Result<User> Create(
        Guid id,
        string firstName,
        string lastName,
        string? fatherName,
        string phoneNumber,
        DateOnly dateOfBirth,
        Guid? imageId,
        string email,
        string? description,
        string passwordHash)
    {
        if (string.IsNullOrEmpty(firstName) || firstName.Length > MaxLength)
        {
            return Result.Failure<User>($"'{nameof(firstName)}' cannot be null, empty or more than {MaxLength} characters.");
        }
        if (string.IsNullOrEmpty(lastName) || lastName.Length > MaxLength)
        {
            return Result.Failure<User>($"'{nameof(lastName)}' cannot be null, empty or more than {MaxLength} characters.");
        }
        if (!string.IsNullOrEmpty(fatherName) && fatherName.Length > MaxLength)
        {
            return Result.Failure<User>($"'{nameof(fatherName)}' cannot be more than {MaxLength} characters.");
        }
        if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length > MaxLength)
        {
            return Result.Failure<User>($"'{nameof(phoneNumber)}' cannot be null, empty or more than {MaxLength} characters.");
        }
        if (string.IsNullOrEmpty(email) || email.Length > MaxLength)
        {
            return Result.Failure<User>($"'{nameof(email)}' cannot be null, empty or more than {MaxLength} characters.");
        }
        if (!string.IsNullOrEmpty(description) && description.Length > MaxDescriptionLength)
        {
            return Result.Failure<User>($"'{nameof(description)}' cannot be more than {MaxDescriptionLength} characters.");
        }
        if (dateOfBirth.CompareTo(MaxDate) > 0 || dateOfBirth.CompareTo(MinDate) < 0)
        {
            return Result.Failure<User>($"'{nameof(dateOfBirth)}' is out of range. Must be between {MinDate.ToString("yyyy-MM-dd")} and {MaxDate.ToString("yyyy-MM-dd")}.");
        }

        var user = new User(
            id,
            firstName,
            lastName,
            fatherName,
            phoneNumber,
            dateOfBirth,
            imageId,
            email,
            description,
            passwordHash);

        return Result.Success(user);
    }
}