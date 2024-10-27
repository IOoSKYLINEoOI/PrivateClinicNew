using System;

namespace Clinic.Web.Contracts.TimeSlot;

public record TimeSlotUpdateRequest(
    TimeOnly StartTime, 
    TimeOnly EndTime, 
    bool IsAvailable);


