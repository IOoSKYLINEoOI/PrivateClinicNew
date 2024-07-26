namespace Clinic.DataAccess.Models;

public class UserEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? FatherName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    public ICollection<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    public ICollection<ReceptionEntity> Receptions { get; set; } = new List<ReceptionEntity>();
    public Guid? AddressId { get; set; }
    public AddressEntity? Address { get; set; }

    public Guid? ImageId { get; set; }
    public ImageEntity? Image { get; set; }

    public EmployeeEntity? Employee { get; set; }
}
