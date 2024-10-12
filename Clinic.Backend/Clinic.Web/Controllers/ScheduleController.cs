using Clinic.Core.Interfaces.Services;
using Clinic.Core.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class SchedulesController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public SchedulesController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Schedule>> GetScheduleById(Guid id)
    {
        var result = await _scheduleService.GetByIdSchedule(id);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<ActionResult<List<Schedule>>> GetAllSchedules()
    {
        var result = await _scheduleService.GetAllSchedules();

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult> CreateSchedule([FromBody] Schedule schedule)
    {
        var result = await _scheduleService.AddSchedule(schedule);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(schedule);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateSchedule(Guid id, [FromBody] Schedule schedule)
    {
        if (id != schedule.Id)
        {
            return BadRequest("Schedule ID mismatch.");
        }

        var result = await _scheduleService.UpdateSchedule(schedule);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteSchedule(Guid id)
    {
        var result = await _scheduleService.DeleteSchedule(id);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(id);
    }
}
