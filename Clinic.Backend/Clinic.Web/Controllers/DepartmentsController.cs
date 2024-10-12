﻿using Clinic.Application.Services;
using Clinic.Core.Models;
using Clinic.Web.Contracts.Addresses;
using Clinic.Web.Contracts.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;

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
    [HttpPost]
    public async Task<ActionResult> CreateDepartment([FromBody] DepartmentRequest request)
    {
        // Проверяем, что запрос не null
        if (request == null)
        {
            return BadRequest("Invalid department request");
        }

        // Создаем адрес
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

        // Создаем департамент
        var resDepartment = Department.Create(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            resAddress.Value.Id 
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
    public async Task<ActionResult<Guid>> UpdateDepartment(Guid id, [FromBody] DepartmentRequest request)
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
            resAddress.Value.Id 
        );

        if (resDepartment.IsFailure)
        {
            return BadRequest(resDepartment.Error);
        }

        var addResult = await _departmentService.UpdateDepartment(resDepartment.Value, resAddress.Value);
        if (addResult.IsFailure)
        {
            return BadRequest(addResult.Error);
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