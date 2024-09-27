namespace Clinic.DataAccess.Models;

public class AppointmentEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid? ReceptionId { get; set; }
    public Guid TimeSlotId { get; set; }
    public DateTime DateOfBooking { get; set; }
    public int StatusAppointmentId { get; set; }

    public UserEntity? User { get; set; }
    public ReceptionEntity? Reception { get; set; }
    public TimeSlotEntity? TimeSlot { get; set; }
    public StatusAppointmentEntity? StatusAppointment { get; set; }
}
