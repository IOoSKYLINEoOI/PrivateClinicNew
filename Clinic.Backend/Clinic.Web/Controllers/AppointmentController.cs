using Clinic.Core.Interfaces.Services;
using Clinic.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = "GetAppointment")]
    public async Task<ActionResult<Appointment>> GetAppointmentById(Guid id)
    {
        var result = await _appointmentService.GetByAppointmentId(id);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost]
    [Authorize(Policy = "CreateAppointment")]
    public async Task<ActionResult> CreateAppointment([FromBody] Appointment appointment)
    {
        var result = await _appointmentService.AddAppointment(appointment);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(appointment);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "UpdateAppointment")]
    public async Task<ActionResult> UpdateAppointment([FromBody] Appointment updatedAppointment)
    {
        var result = await _appointmentService.UpdateAppointment(updatedAppointment);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(updatedAppointment.Id);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "DeleteAppointment")]
    public async Task<ActionResult> DeleteAppointment(Guid id)
    {
        var result = await _appointmentService.DeleteAppointment(id);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(id);
    }
}
