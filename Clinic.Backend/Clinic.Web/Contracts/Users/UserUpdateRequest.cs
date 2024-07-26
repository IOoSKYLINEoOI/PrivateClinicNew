using System.ComponentModel.DataAnnotations;

namespace Clinic.Web.Contracts.Users;

public record UserUpdateRequest(
         [Required] string FirstName,
         [Required] string LastName,
         string? FatherName,
         [Required] DateOnly DateOfBirth,
         Guid? AddressId,
         IFormFile? FileName);
