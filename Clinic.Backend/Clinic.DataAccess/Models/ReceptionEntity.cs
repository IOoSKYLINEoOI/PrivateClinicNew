using Clinic.DataAccess.Models;

public class ReceptionEntity
{
    public Guid Id { get; set; }
    public DateTime DateReceipt { get; set; }
    public DateTime? DateOfReturn { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid EmployeeId { get; set; }

    public UserEntity? User { get; set; }
    public DepartmentEntity? Department { get; set; }
    public EmployeeEntity? Employee { get; set; }
    public ICollection<ResultICDEntity> Results { get; set; } = new List<ResultICDEntity>();
    public ICollection<AppointmentEntity> Appointments { get; set; } = new List<AppointmentEntity>();
}
