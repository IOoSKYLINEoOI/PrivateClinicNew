using Clinic.Core.Models;
using CSharpFunctionalExtensions;

namespace Clinic.Application.Services;

public interface IEmployeeService
{
    Task<Result> AddEmployee(Employee employee);
    Task<Result> DeleteEmployee(Guid id);
    Task<Result<List<Employee>>> GetAllEmployee();
    Task<Result> UpdateEmployee(Guid id, DateOnly hiringDate, DateOnly? dateOfDismissal, string? description, Guid userId);
}