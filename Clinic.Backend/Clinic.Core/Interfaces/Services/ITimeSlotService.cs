using Clinic.Core.Models;
using CSharpFunctionalExtensions;

namespace Clinic.Core.Interfaces.Services;

public interface ITimeSlotService
{
    Task<Result> AddTimeSlot(TimeSlot timeSlot);
    Task<Result> DeleteTimeSlot(Guid id);
    Task<Result<List<TimeSlot>>> GetAllTimeSlots();
    Task<Result<TimeSlot>> GetTimeSlotById(Guid id);
    Task<Result> UpdateTimeSlot(Guid id, TimeOnly startTime, TimeOnly endTime, bool isAvailable);
}