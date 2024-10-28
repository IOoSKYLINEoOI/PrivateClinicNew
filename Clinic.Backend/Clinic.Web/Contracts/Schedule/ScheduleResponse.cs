namespace Clinic.Web.Contracts.Schedule;

public record ScheduleResponse(
    Guid id,
    Guid employeeId,
    DateOnly workDate,
    TimeOnly startTime,
    TimeOnly endTime);

