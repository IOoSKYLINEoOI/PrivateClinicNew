namespace Clinic.Web.Contracts.Appointment;

public record AppointmentResponse(
    Guid id,
    Guid userId,
    Guid? receptionId,
    Guid timeSlotId,
    DateTime dateOfBooking,
    int statusAppointmentId);
