using System.ComponentModel.DataAnnotations;

namespace Clinic.Web.Contracts.Addresses;

public record AddressRequest(
    [Required] string Country,
    [Required] string Region,
    [Required] string City,
    [Required] string Street,
    [Required] int HouseNumber,
    [Required] int ApartmentNumber,
    string? Description,
    string? Pavilion);
