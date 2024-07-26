using Clinic.Application.Services;
using Clinic.Core.Models;
using Clinic.DataAccess.Repositories;
using CSharpFunctionalExtensions;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeesRepository _emloyeeRepository;

    public EmployeeService(IEmployeesRepository emloyeeRepository)
    {
        _emloyeeRepository = emloyeeRepository;
    }

    public async Task<Result> AddEmployee(Employee employee)
    {
        await _emloyeeRepository.Add(employee);
        return Result.Success();
    }

    public async Task<Result> DeleteEmployee(Guid id)
    {
        await _emloyeeRepository.Delete(id);
        return Result.Success();
    }

    public async Task<Result<List<Employee>>> GetAllEmployee()
    {
        var employees = await _emloyeeRepository.GetAll();
        return Result.Success(employees);
    }

    public async Task<Result> UpdateEmployee(Guid id, DateOnly hiringDate, DateOnly? dateOfDismissal, string? description, Guid userId)
    {
        await _emloyeeRepository.Update(id, hiringDate, dateOfDismissal, description, userId);
        return Result.Success();
    }
}