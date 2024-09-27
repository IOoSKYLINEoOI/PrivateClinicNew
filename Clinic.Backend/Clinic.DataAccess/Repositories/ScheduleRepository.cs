using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using Clinic.DataAccess.Models;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Clinic.DataAccess.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly ClinicDbContext _context;

    public ScheduleRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Add(Schedule schedule)
    {
        var scheduleEntity = new ScheduleEntity
        {
            Id = schedule.Id,
            EmployeeId = schedule.EmployeeId,
            WorkDate = schedule.WorkDate,
            StartTime = schedule.StartTime,
            EndTime = schedule.EndTime
        };

        await _context.Schedules.AddAsync(scheduleEntity);
        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<Schedule>> GetById(Guid id)
    {
        var scheduleEntity = await _context.Schedules
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (scheduleEntity == null)
            return Result.Failure<Schedule>($"Schedule with ID {id} not found.");

        var schedule = Schedule.Create(
            scheduleEntity.Id,
            scheduleEntity.EmployeeId,
            scheduleEntity.WorkDate,
            scheduleEntity.StartTime,
            scheduleEntity.EndTime).Value;

        return Result.Success(schedule);
    }

    public async Task<Result<List<Schedule>>> GetAll()
    {
        var scheduleEntities = await _context.Schedules
            .AsNoTracking()
            .ToListAsync();

        var schedules = scheduleEntities
            .Select(se => Schedule.Create(
                se.Id,
                se.EmployeeId,
                se.WorkDate,
                se.StartTime,
                se.EndTime).Value)
            .ToList();

        return Result.Success(schedules);
    }

    public async Task<Result> Update(Schedule schedule)
    {
        var scheduleEntity = await _context.Schedules.FindAsync(schedule.Id);
        if (scheduleEntity == null)
            return Result.Failure($"Schedule with ID {schedule.Id} not found.");

        scheduleEntity.WorkDate = schedule.WorkDate;
        scheduleEntity.StartTime = schedule.StartTime;
        scheduleEntity.EndTime = schedule.EndTime;

        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> Delete(Guid id)
    {
        var scheduleEntity = await _context.Schedules.FindAsync(id);
        if (scheduleEntity == null)
            return Result.Failure($"Schedule with ID {id} not found.");

        _context.Schedules.Remove(scheduleEntity);
        await _context.SaveChangesAsync();

        return Result.Success();
    }
}
