namespace Clinic.DataAccess.Models;

public class DepartmentEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public Guid AddressId { get; set; }
    public AddressEntity? Address { get; set; }

    public ICollection<EmployeeEntity> Employees { get; set; } = new List<EmployeeEntity>();
    public ICollection<ReceptionEntity> Receptions { get; set; } = new List<ReceptionEntity>();
}
