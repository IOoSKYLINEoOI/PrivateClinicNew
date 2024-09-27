using CSharpFunctionalExtensions;

namespace Clinic.Core.Models;

public class TimeSlot
{
    private TimeSlot(
        Guid id,
        Guid scheduleId,
        TimeOnly startTime,
        TimeOnly endTime,
        bool isAvaliable)
    {
        Id = id;
        ScheduleId = scheduleId;
        StartTime = startTime;
        EndTime = endTime;
        IsAvailable = isAvaliable;
    }

    public Guid Id { get; }
    public Guid ScheduleId { get; }
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }
    public bool IsAvailable { get; }

    public static Result<TimeSlot> Create(
        Guid id,
        Guid scheduleId,
        TimeOnly startTime,
        TimeOnly endTime,
        bool isAvaliable)
    {
        if (startTime >= endTime)
        {
            return Result.Failure<TimeSlot>("StartTime must be earlier than EndTime.");
        }

        var timeSlot = new TimeSlot(
            id,
            scheduleId,
            startTime,
            endTime,
            isAvaliable);

        return Result.Success(timeSlot);
    }
}
