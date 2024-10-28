using System.ComponentModel.DataAnnotations;

namespace Clinic.Web.Contracts.Appointment;

public record AppointmentRequest(
    [Required] Guid userId,
    Guid? receptionId,
    [Required] Guid timeSlotId,
    [Required] DateTime dateOfBooking,
    [Required] int statusAppointmentId);

