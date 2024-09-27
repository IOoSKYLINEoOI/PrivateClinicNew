using Clinic.Core.Models;
using Clinic.DataAccess.Models;
using Clinic.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Clinic.DataAccess.Repositories;

public class TimeSlotRepository : ITimeSlotRepository
{
    private readonly ClinicDbContext _context;

    public TimeSlotRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task Add(TimeSlot timeSlot)
    {
        var schedule = await _context.Schedules
            .FirstOrDefaultAsync(s => s.Id == timeSlot.ScheduleId)
            ?? throw new Exception($"Schedule with ID {timeSlot.ScheduleId} not found.");

        var timeSlotEntity = new TimeSlotEntity
        {
            Id = timeSlot.Id,
            ScheduleId = timeSlot.ScheduleId,
            StartTime = timeSlot.StartTime,
            EndTime = timeSlot.EndTime,
            IsAvailable = timeSlot.IsAvailable
        };

        await _context.TimeSlots.AddAsync(timeSlotEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<TimeSlot?> GetById(Guid id)
    {
        var timeSlotEntity = await _context.TimeSlots
            .AsNoTracking()
            .FirstOrDefaultAsync(ts => ts.Id == id);

        if (timeSlotEntity == null)
        {
            return null;
        }

        return TimeSlot.Create(
            timeSlotEntity.Id,
            timeSlotEntity.ScheduleId,
            timeSlotEntity.StartTime,
            timeSlotEntity.EndTime,
            timeSlotEntity.IsAvailable).Value;
    }

    public async Task<List<TimeSlot>> GetAll()
    {
        var timeSlotEntities = await _context.TimeSlots
            .AsNoTracking()
            .ToListAsync();

        var timeSlots = timeSlotEntities
            .Select(ts => TimeSlot.Create(
                ts.Id,
                ts.ScheduleId,
                ts.StartTime,
                ts.EndTime,
                ts.IsAvailable).Value)
            .ToList();

        return timeSlots;
    }

    public async Task Update(
        Guid id,
        TimeOnly startTime,
        TimeOnly endTime,
        bool isAvailable)
    {
        await _context.TimeSlots
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(ts => ts
                .SetProperty(x => x.StartTime, startTime)
                .SetProperty(x => x.EndTime, endTime)
                .SetProperty(x => x.IsAvailable, isAvailable));
    }

    public async Task Delete(Guid id)
    {
        await _context.TimeSlots
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
}
