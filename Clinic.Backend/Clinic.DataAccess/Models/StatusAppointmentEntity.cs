namespace Clinic.DataAccess.Models;

public class StatusAppointmentEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<AppointmentEntity> Appointments { get; set; } = new List<AppointmentEntity>();
}
