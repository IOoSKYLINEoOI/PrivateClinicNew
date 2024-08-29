using Clinic.Core.Models;

namespace Clinic.Core.Interfaces.Repositories;

public interface IEmployeesDepartmentsRepository
{
    Task Add(EmployeeDepartment employeeDepartment);
    Task Delete(Guid employeeId, Guid departmentId);
    Task<EmployeeDepartment> GetById(Guid employeeId, Guid departmentId);
    Task Update(Guid employeeId, Guid departmentId, string? description, Guid positionId);
}