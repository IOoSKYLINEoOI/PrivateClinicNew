using Clinic.Core.Models;
using CSharpFunctionalExtensions;

namespace Clinic.Core.Interfaces.Services;

public interface IAppointmentService
{
    Task<Result> AddAppointment(Appointment appointment);
    Task<Result> DeleteAppointment(Guid id);
    Task<Result<Appointment>> GetByAppointmentId(Guid id);
    //Task<Result> UpdateAppointment(Guid id, Appointment updatedAppointment);
}
