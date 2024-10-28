namespace Clinic.Web.Contracts.TimeSlot;

public record TimeSlotResponse(
    Guid id,
    TimeOnly StartTime,
    TimeOnly EndTime,
    bool IsAvailable);


