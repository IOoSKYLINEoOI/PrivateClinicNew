using System.ComponentModel.DataAnnotations;

namespace Clinic.Web.Contracts.Users;

public record LoginUserRequest(
    [Required] string Email,
    [Required] string Password);
