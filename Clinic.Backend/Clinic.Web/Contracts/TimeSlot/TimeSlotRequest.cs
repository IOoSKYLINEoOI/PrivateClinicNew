namespace Clinic.Web.Contracts.TimeSlot;

public record TimeSlotRequest(
    TimeOnly StartTime, 
    TimeOnly EndTime, 
    bool IsAvailable);


