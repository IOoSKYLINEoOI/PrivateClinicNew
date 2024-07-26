using System.ComponentModel.DataAnnotations;

namespace Clinic.Web.Contracts.Users;

public record RegisterUserRequest(
    [Required] string FirstName,
    [Required] string LastName,
    string FatherName,
    [Required] DateOnly DateOfBirth,
    [Required] string Email,
    [Required] string PhoneNumber,
    [Required] string Password);

