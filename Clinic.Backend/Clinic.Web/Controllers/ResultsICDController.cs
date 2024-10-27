using Clinic.Application.Services;
using Clinic.Core.Models;
using Clinic.Web.Contracts.ResultsICD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ResultsICDController : ControllerBase
{
    private readonly IResultICDService _resultICDService;

    public ResultsICDController(IResultICDService resultICDService)
    {
        _resultICDService = resultICDService;
    }

    [HttpGet("{receptionId:guid}")]
    [Authorize(Policy = "GetAllResultICDReception")]
    public async Task<ActionResult<List<ResultICDResponse>>> GetAllResultICD(Guid receptionId)
    {
        var result = await _resultICDService.GetResultICD(receptionId);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        var response = result.Value.Select(y => new ResultICDResponse(y.Id, y.ICDCode, y.Description, y.ReceptionId)).ToList();

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "CreateResultICD")]
    public async Task<ActionResult> CreateResultICD([FromBody] ResultICDRequest request)
    {
        var res = ResultICD.Create(
            Guid.NewGuid(),
            request.ICDCode,
            request.Description,
            request.ReceptionId);

        if (res.IsFailure)
        {
            return BadRequest(res.Error);
        }

        var addResult = await _resultICDService.AddResultICD(res.Value);
        if (addResult.IsFailure)
        {
            return BadRequest(addResult.Error);
        }

        return Ok(res.Value);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "UpdateResultICD")]
    public async Task<ActionResult<Guid>> UpdateResultICD(Guid id, [FromBody] ResultICDRequest request)
    {
        var result = await _resultICDService.UpdateResultICD(
            id,
            request.ICDCode,
            request.Description,
            request.ReceptionId);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(id);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "DeleteResultICD")]
    public async Task<ActionResult<Guid>> DeleteResultICD(Guid id)
    {
        var result = await _resultICDService.DeleteResultICD(id);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(id);
    }
}