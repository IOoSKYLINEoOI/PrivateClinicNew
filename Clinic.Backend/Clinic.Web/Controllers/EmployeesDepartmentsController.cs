using Clinic.Application.Services;
using Clinic.Core.Models;
using Clinic.Web.Contracts.EmployeesDepartments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class EmployeesDepartmentsController : ControllerBase
{
    private readonly IEmployeeDepartmentService _employeeDepartmentService;

    public EmployeesDepartmentsController(IEmployeeDepartmentService employeeDepartmentService)
    {
        _employeeDepartmentService = employeeDepartmentService;
    }

    [HttpGet]
    [Authorize(Policy = "GetEmployeeDepartment")]
    public async Task<ActionResult<EmployeeDepartmentResponse>> GetByEmployeeDepartment(Guid employeeId, Guid departmentId)
    {
        var result = await _employeeDepartmentService.GetByEmployeeDepartment(employeeId, departmentId);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost]
    [Authorize(Policy = "CreateEmployeeDepartment")]
    public async Task<ActionResult> CreateEmployeeDepartment([FromBody] EmployeeDepartmentRequest request)
    {
        var res = EmployeeDepartment.Create(
            request.EmployeeId,
            request.DepartmentId,
            request.Description,
            request.PositionId);

        if (res.IsFailure)
        {
            return BadRequest(res.Error);
        }

        var addResult = await _employeeDepartmentService.AddEmployeeDepartment(res.Value);
        if (addResult.IsFailure)
        {
            return BadRequest(addResult.Error);
        }

        return Ok(res.Value);
    }

    [HttpPut]
    [Authorize(Policy = "UpdateEmployeeDepartment")]
    public async Task<ActionResult<(Guid, Guid)>> UpdateEmployeeDepartment(Guid employeeId, Guid departmentId, [FromBody] EmployeeDepartmentRequest request)
    {
        var result = await _employeeDepartmentService.UpdateEmployeeDepartment(
            employeeId,
            departmentId,
            request.Description,
            request.PositionId);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok((employeeId, departmentId));
    }

    [HttpDelete("{employeeId:guid}/{departmentId:guid}")]
    [Authorize(Policy = "DeleteEmployeeDepartment")]
    public async Task<ActionResult<(Guid, Guid)>> DeleteEmployeeDepartment(Guid employeeId, Guid departmentId)
    {
        var result = await _employeeDepartmentService.DeleteEmployeeDepartment(employeeId, departmentId);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok((employeeId, departmentId));
    }
}