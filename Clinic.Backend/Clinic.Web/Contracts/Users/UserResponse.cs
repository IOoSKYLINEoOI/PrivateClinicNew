namespace Clinic.Web.Contracts.Users;

public record UserResponse(
    Guid id,
    string firstName,
    string lastName,
    string? fatherName,
    string phoneNumber,
    DateOnly dateOfBirth,
    Guid? imageId,
    string email,
    string? description);
