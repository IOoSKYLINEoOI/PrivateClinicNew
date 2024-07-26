using Clinic.Application.Services;
using Clinic.Core.Models;
using Clinic.Web.Contracts.Employee;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<EmployeeResponse>>> GetAllEmployee()
    {
        var result = await _employeeService.GetAllEmployee();
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        var response = result.Value.Select(y => new EmployeeResponse(y.Id, y.HiringDate, y.DateOfDismissal, y.Description, y.UserId)).ToList();

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> CreateEmployee([FromBody] EmployeeRequest request)
    {
        var res = Employee.Create(
            Guid.NewGuid(),
            request.HiringDate,
            request.DateOfDismissal,
            request.Description,
            request.UserId);

        if (res.IsFailure)
        {
            return BadRequest(res.Error);
        }

        var addResult = await _employeeService.AddEmployee(res.Value);
        if (addResult.IsFailure)
        {
            return BadRequest(addResult.Error);
        }

        return Ok(res.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateEmployee(Guid id, [FromBody] EmployeeRequest request)
    {
        var result = await _employeeService.UpdateEmployee(
            id,
            request.HiringDate,
            request.DateOfDismissal,
            request.Description,
            request.UserId);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteEmployee(Guid id)
    {
        var result = await _employeeService.DeleteEmployee(id);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(id);
    }
}