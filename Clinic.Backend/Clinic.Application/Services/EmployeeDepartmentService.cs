using Clinic.Application.Services;
using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using CSharpFunctionalExtensions;

public class EmployeeDepartmentService : IEmployeeDepartmentService
{
    private readonly IEmployeesDepartmentsRepository _employeesDepartmentsRepository;

    public EmployeeDepartmentService(IEmployeesDepartmentsRepository employeesDepartmentsRepository)
    {
        _employeesDepartmentsRepository = employeesDepartmentsRepository;
    }

    public async Task<Result> AddEmployeeDepartment(EmployeeDepartment employeeDepartment)
    {
        await _employeesDepartmentsRepository.Add(employeeDepartment);
        return Result.Success();
    }

    public async Task<Result> DeleteEmployeeDepartment(Guid employeeId, Guid departmentId)
    {
        await _employeesDepartmentsRepository.Delete(employeeId, departmentId);
        return Result.Success();
    }

    public async Task<Result<EmployeeDepartment>> GetByEmployeeDepartment(Guid employeeId, Guid departmentId)
    {
        var employeeDepartment = await _employeesDepartmentsRepository.GetById(employeeId, departmentId);
        return employeeDepartment != null ? Result.Success(employeeDepartment) : Result.Failure<EmployeeDepartment>("EmployeeDepartment not found");
    }

    public async Task<Result> UpdateEmployeeDepartment(Guid employeeId, Guid departmentId, string? description, int positionId)
    {
        await _employeesDepartmentsRepository.Update(employeeId, departmentId, description, positionId);
        return Result.Success();
    }
}