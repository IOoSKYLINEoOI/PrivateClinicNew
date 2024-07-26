namespace Clinic.DataAccess.Models;

public class AddressEntity
{
    public Guid Id { get; set; }
    public string Country { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int HouseNumber { get; set; }
    public string? Pavilion { get; set; }
    public int ApartmentNumber { get; set; }
    public string? Description { get; set; }

    public DepartmentEntity? Department { get; set; }
    public UserEntity? User { get; set; }
}
