using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Interfaces.Services;
using Clinic.Core.Models;
using CSharpFunctionalExtensions;

namespace Clinic.Application.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<Result> AddAppointment(Appointment appointment)
    {
        await _appointmentRepository.Add(appointment);
        return Result.Success();
    }

    public async Task<Result> DeleteAppointment(Guid id)
    {
        await _appointmentRepository.Delete(id);
        return Result.Success();
    }

    public async Task<Result<Appointment>> GetByAppointmentId(Guid id)
    {
        var apointment = await _appointmentRepository.GetById(id);
        return apointment != null ? Result.Success(apointment) : Result.Failure<Appointment>("Apointment not found");
    }

    public async Task<Result> UpdateAppointment(Appointment updatedAppointment)
    {
        await _appointmentRepository.Update(
            updatedAppointment.Id, 
            updatedAppointment.UserId, 
            updatedAppointment.ReceptionId, 
            updatedAppointment.TimeSlotId, 
            updatedAppointment.DateOfBooking, 
            updatedAppointment.StatusAppointmentId);

        return Result.Success();
    }
}
