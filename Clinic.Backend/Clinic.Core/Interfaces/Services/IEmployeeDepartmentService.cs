using Clinic.Core.Models;
using CSharpFunctionalExtensions;

namespace Clinic.Application.Services;

public interface IEmployeeDepartmentService
{
    Task<Result> AddEmployeeDepartment(EmployeeDepartment employeeDepartment);
    Task<Result> DeleteEmployeeDepartment(Guid employeeId, Guid departmentId);
    Task<Result<EmployeeDepartment>> GetByEmployeeDepartment(Guid employeeId, Guid departmentId);
    Task<Result> UpdateEmployeeDepartment(Guid employeeId, Guid departmentId, string? description, int positionId);
}