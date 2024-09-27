using CSharpFunctionalExtensions;

namespace Clinic.Core.Models;

public class Schedule
{
    private Schedule(
        Guid id,
        Guid employeeId,
        DateOnly workDate,
        TimeOnly startTime,
        TimeOnly endTime)
    {
        Id = id;
        EmployeeId = employeeId;
        WorkDate = workDate;
        StartTime = startTime;
        EndTime = endTime;
    }

    public Guid Id { get; }
    public Guid EmployeeId { get;}
    public DateOnly WorkDate { get; }
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }

    public static Result<Schedule> Create(
        Guid id,
        Guid employeeId,
        DateOnly workDate,
        TimeOnly startTime,
        TimeOnly endTime)
    {
        if (startTime >= endTime)
        {
            return Result.Failure<Schedule>("StartTime must be earlier than EndTime.");
        }

        var schedule = new Schedule(
            id,
            employeeId,
            workDate,
            startTime,
            endTime);

        return Result.Success(schedule);
    }
}
