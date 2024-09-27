using CSharpFunctionalExtensions;

namespace Clinic.Core.Models;

public class Appointment
{
    private Appointment(
        Guid id,
        Guid userId,
        Guid? receptionId,
        Guid timeSlotId,
        DateTime dateOfBooking,
        int statusAppointmentId)
    {
        Id = id;
        UserId = userId;
        ReceptionId = receptionId;
        TimeSlotId = timeSlotId;
        DateOfBooking = dateOfBooking;
        StatusAppointmentId = statusAppointmentId;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public Guid? ReceptionId { get; }
    public Guid TimeSlotId { get; }
    public DateTime DateOfBooking { get; }
    public int StatusAppointmentId { get; }

    public static Result<Appointment> Create(
        Guid id,
        Guid userId,
        Guid? receptionId,
        Guid timeSlotId,
        DateTime dateOfBooking,
        int statusAppointmentId)
    {
        if (userId == Guid.Empty)
            return Result.Failure<Appointment>("User ID must not be empty.");
        if (timeSlotId == Guid.Empty)
            return Result.Failure<Appointment>("Time Slot ID must not be empty.");
        if (dateOfBooking < DateTime.Now)
            return Result.Failure<Appointment>("Date of booking cannot be in the past.");

        var appointment = new Appointment(
            id,
            userId,
            receptionId,
            timeSlotId,
            dateOfBooking,
            statusAppointmentId);

        return Result.Success(appointment);
    }
}
