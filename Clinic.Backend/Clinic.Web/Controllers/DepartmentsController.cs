using Clinic.Application.Services;
using Clinic.Core.Models;
using Clinic.Web.Contracts.Addresses;
using Clinic.Web.Contracts.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
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

        var response = result.Value.Select(department => new DepartmentResponse(
            department.Id,
            department.Name,
            department.Description,
            new AddressResponse(
                department.Address.Id,
                department.Address.Country,
                department.Address.Region,
                department.Address.City,
                department.Address.Street,
                department.Address.HouseNumber,
                department.Address.ApartmentNumber,
                department.Address.Description,
                department.Address.Pavilion
            )
        )).ToList();

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "CreateDepartment")]
    public async Task<ActionResult> CreateDepartment([FromBody] DepartmentRequest request)
    {

        if (request == null)
        {
            return BadRequest("Invalid department request");
        }

        var resAddress = Address.Create(
            Guid.NewGuid(),
            request.Address.Country,
            request.Address.Region,
            request.Address.City,
            request.Address.Street,
            request.Address.HouseNumber,
            request.Address.ApartmentNumber,
            request.Address.Description,
            request.Address.Pavilion
        );

        if (resAddress.IsFailure)
        {
            return BadRequest(resAddress.Error);
        }

        var resDepartment = Department.Create(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            resAddress.Value 
        );

        if (resDepartment.IsFailure)
        {
            return BadRequest(resDepartment.Error);
        }

        var addResult = await _departmentService.AddDepartment(resDepartment.Value, resAddress.Value);
        if (addResult.IsFailure)
        {
            return BadRequest(addResult.Error);
        }

        return Ok(resDepartment.Value.Id);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "UpdateDepartment")]
    public async Task<ActionResult<Guid>> UpdateDepartment(Guid id, [FromBody] DepartmentRequest request)
    {

        var resAddress = Address.Create(
            id,
            request.Address.Country,
            request.Address.Region,
            request.Address.City,
            request.Address.Street,
            request.Address.HouseNumber,
            request.Address.ApartmentNumber,
            request.Address.Description,
            request.Address.Pavilion
        );

        if (resAddress.IsFailure)
        {
            return BadRequest(resAddress.Error);
        }

        var resDepartment = Department.Create(
            id, 
            request.Name,
            request.Description,
            resAddress.Value
        );

        if (resDepartment.IsFailure)
        {
            return BadRequest(resDepartment.Error);
        }

        var updateResult = await _departmentService.UpdateDepartment(resDepartment.Value, resAddress.Value);
        if (updateResult.IsFailure)
        {
            return BadRequest(updateResult.Error);
        }

        return Ok(id);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "DeleteDepartment")]
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
