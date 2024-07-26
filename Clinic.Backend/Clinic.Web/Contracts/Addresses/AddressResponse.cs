namespace Clinic.Web.Contracts.Addresses;

public record AddressResponse(
    Guid Id,
    string Country,
    string Region,
    string City,
    string Street,
    int HouseNumber,
    int ApartmentNumber,
    string? Description,
    string? Pavilion);

