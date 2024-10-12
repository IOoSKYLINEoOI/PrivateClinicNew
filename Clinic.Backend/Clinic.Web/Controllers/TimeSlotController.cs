using Clinic.Core.Interfaces.Services;
using Clinic.Core.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class TimeSlotsController : ControllerBase
{
    private readonly ITimeSlotService _timeSlotService;

    public TimeSlotsController(ITimeSlotService timeSlotService)
    {
        _timeSlotService = timeSlotService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TimeSlot>> GetTimeSlotById(Guid id)
    {
        var result = await _timeSlotService.GetTimeSlotById(id);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<ActionResult<List<TimeSlot>>> GetAllTimeSlots()
    {
        var result = await _timeSlotService.GetAllTimeSlots();

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult> CreateTimeSlot([FromBody] TimeSlot timeSlot)
    {
        var result = await _timeSlotService.AddTimeSlot(timeSlot);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(timeSlot);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateTimeSlot(Guid id, [FromBody] TimeSlotUpdateRequest request)
    {
        var result = await _timeSlotService.UpdateTimeSlot(id, request.StartTime, request.EndTime, request.IsAvailable);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteTimeSlot(Guid id)
    {
        var result = await _timeSlotService.DeleteTimeSlot(id);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(id);
    }
}

public class TimeSlotUpdateRequest
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public bool IsAvailable { get; set; }
}
