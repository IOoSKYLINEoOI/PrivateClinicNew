namespace Clinic.DataAccess.Models;

public class TimeSlotEntity
{
    public Guid Id { get; set; }
    public Guid ScheduleId { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public bool IsAvailable { get; set; }

    public ScheduleEntity? Schedule { get; set; }
    public AppointmentEntity? Appointment { get; set; }
}
