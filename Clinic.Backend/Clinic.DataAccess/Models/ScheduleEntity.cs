namespace Clinic.DataAccess.Models;

public class ScheduleEntity
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateOnly WorkDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public EmployeeEntity? Employee { get; set; }
    public ICollection<TimeSlotEntity> TimeSlots { get; set; } = new List<TimeSlotEntity>();
}
