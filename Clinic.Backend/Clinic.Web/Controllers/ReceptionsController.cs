using Clinic.Application.Services;
using Clinic.Core.Models;
using Clinic.Web.Contracts.Receptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ReceptionsController : ControllerBase
{
    private readonly IReceptionService _receptionService;

    public ReceptionsController(IReceptionService receptionService)
    {
        _receptionService = receptionService;
    }

    [HttpGet("{userId:guid}")]
    [Authorize(Policy = "GetAllReception")]
    public async Task<ActionResult<List<ReceptionResponse>>> GetAllReceptionUser(Guid userId)
    {
        var result = await _receptionService.GetAllReceptionUser(userId);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        var response = result.Value.Select(y => new ReceptionResponse(y.Id, y.DateReceipt, y.DateOfReturn, y.Description, y.UserId, y.DepartmentId, y.EmployeeId)).ToList();

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "CreateReception")]
    public async Task<ActionResult> CreateReception([FromBody] ReceptionRequest request)
    {
        var res = Reception.Create(
            Guid.NewGuid(),
            request.DateReceipt,
            request.DateOfReturn,
            request.Description,
            request.UserId,
            request.DepartmentId,
            request.EmployeeId);

        if (res.IsFailure)
        {
            return BadRequest(res.Error);
        }

        var addResult = await _receptionService.AddReception(res.Value);
        if (addResult.IsFailure)
        {
            return BadRequest(addResult.Error);
        }

        return Ok(res.Value);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "UpdateReception")]
    public async Task<ActionResult<Guid>> UpdateReception(Guid id, [FromBody] ReceptionRequest request)
    {
        var result = await _receptionService.UpdateReception(
            id,
            request.DateReceipt,
            request.DateOfReturn,
            request.Description,
            request.UserId,
            request.DepartmentId,
            request.EmployeeId);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(id);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "DeleteReception")]
    public async Task<ActionResult<Guid>> DeleteReception(Guid id)
    {
        var result = await _receptionService.DeleteReception(id);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(id);
    }
}