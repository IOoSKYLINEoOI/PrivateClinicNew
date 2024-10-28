using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Interfaces.Services;
using Clinic.Core.Models;
using CSharpFunctionalExtensions;

namespace Clinic.Application.Services;

public class TimeSlotService : ITimeSlotService
{
    private readonly ITimeSlotRepository _timeSlotRepository;

    public TimeSlotService(ITimeSlotRepository timeSlotRepository)
    {
        _timeSlotRepository = timeSlotRepository;
    }

    public async Task<Result> AddTimeSlot(TimeSlot timeSlot)
    {
        try
        {
            await _timeSlotRepository.Add(timeSlot);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error while adding time slot: {ex.Message}");
        }
    }

    public async Task<Result<TimeSlot>> GetTimeSlotById(Guid id)
    {
        var timeSlot = await _timeSlotRepository.GetById(id);
        if (timeSlot == null)
        {
            return Result.Failure<TimeSlot>($"Time slot with ID {id} not found.");
        }

        return Result.Success(timeSlot);
    }

    public async Task<Result<List<TimeSlot>>> GetAllTimeSlots()
    {
        var timeSlots = await _timeSlotRepository.GetAll();
        return Result.Success(timeSlots);
    }

    public async Task<Result> UpdateTimeSlot(Guid id, TimeOnly startTime, TimeOnly endTime, bool isAvailable)
    {
        try
        {
            await _timeSlotRepository.Update(id, startTime, endTime, isAvailable);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error while updating time slot: {ex.Message}");
        }
    }

    public async Task<Result> DeleteTimeSlot(Guid id)
    {
        try
        {
            await _timeSlotRepository.Delete(id);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error while deleting time slot: {ex.Message}");
        }
    }
}

