namespace Clinic.DataAccess.Models;

public class EmployeeEntity
{
    public Guid Id { get; set; }
    public DateOnly HiringDate { get; set; }
    public DateOnly? DateOfDismissal { get; set; }
    public string? Description { get; set; }

    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }

    public ICollection<DepartmentEntity> Departments { get; set; } = new List<DepartmentEntity>();
    public ICollection<ReceptionEntity> Receptions { get; set; } = new List<ReceptionEntity>();
}
