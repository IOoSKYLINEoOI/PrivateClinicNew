using Clinic.Application.Services;
using Clinic.Core.Models;
using Clinic.Web.Contracts.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("departments")]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentsController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<DepartmentResponse>>> GetAllDepartments()
    {
        var result = await _departmentService.GetAllDepartment();
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        var response = result.Value.Select(y => new DepartmentResponse(y.Id, y.Name, y.Description, y.AddressId)).ToList();

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateDepartment([FromBody] DepartmentRequest request)
    {
        var res = Department.Create(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.AdressId);

        if (res.IsFailure)
        {
            return BadRequest(res.Error);
        }

        var addResult = await _departmentService.AddDepartment(res.Value);
        if (addResult.IsFailure)
        {
            return BadRequest(addResult.Error);
        }

        return Ok(res.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateDepartment(Guid id, [FromBody] DepartmentRequest request)
    {
        var result = await _departmentService.UpdateDepartment(
            id,
            request.Name,
            request.Description,
            request.AdressId);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteDepartment(Guid id)
    {
        var result = await _departmentService.DeleteDepartment(id);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(id);
    }
}