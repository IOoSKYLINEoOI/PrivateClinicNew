using System.ComponentModel.DataAnnotations;

namespace Clinic.Web.Contracts.Schedule;

public record ScheduleRequest(
    [Required] Guid employeeId,
    [Required] DateOnly workDate,
    [Required] TimeOnly startTime,
    [Required] TimeOnly endTime);

