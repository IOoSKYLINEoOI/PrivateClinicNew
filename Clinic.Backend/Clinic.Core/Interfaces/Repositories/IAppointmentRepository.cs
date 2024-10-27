using Clinic.Core.Models;

namespace Clinic.Core.Interfaces.Repositories;

public interface IAppointmentRepository
{
    Task Add(Appointment appointment);
    Task Update(
            Guid id,
            Guid userId,
            Guid? receptionId,
            Guid timeSlotId,
            DateTime dateOfBooking,
            int statusAppointmentId);
    Task<Appointment?> GetById(Guid id);
    Task Delete(Guid id);
    Task<List<Appointment>> GetAll();
}
