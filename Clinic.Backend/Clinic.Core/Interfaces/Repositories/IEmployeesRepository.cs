using Clinic.Core.Models;

namespace Clinic.Core.Interfaces.Repositories;

public interface IEmployeesRepository
{
    Task Add(Employee employee);
    Task Delete(Guid id);
    Task<List<Employee>> GetAll();
    Task Update(Guid id, DateOnly hiringDate, DateOnly? dateOfDismissal, string? description, Guid userId);
}