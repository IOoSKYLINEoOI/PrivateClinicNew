using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Interfaces.Services;
using Clinic.Core.Models;
using CSharpFunctionalExtensions;

namespace Clinic.Application.Services;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleService(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<Result> AddSchedule(Schedule schedule)
    {
        await _scheduleRepository.Add(schedule);
        return Result.Success();
    }

    public async Task<Result> DeleteSchedule(Guid id)
    {
        await _scheduleRepository.Delete(id);
        return Result.Success();
    }

    public async Task<Result<List<Schedule>>> GetAllSchedules()
    {
        var schedules = await _scheduleRepository.GetAll();
        return Result.Success(schedules);
    }

    public async Task<Result<Schedule>> GetByIdSchedule(Guid id)
    {
        var result = await _scheduleRepository.GetById(id);

        if (result.IsSuccess)
            return Result.Success(result.Value);

        return Result.Failure<Schedule>(result.Error);
    }

    public async Task<Result> UpdateSchedule(Schedule schedule)
    {
        await _scheduleRepository.Update(schedule);
        return Result.Success();
    }
}
