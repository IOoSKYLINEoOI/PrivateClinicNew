using Clinic.Web.Contracts.Addresses;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Web.Contracts.Departments;

public record DepartmentRequest(
    [Required] string Name,
    string? Description,
    AddressRequest Address);
