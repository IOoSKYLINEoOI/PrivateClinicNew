using Clinic.Core.Models;

namespace Clinic.Core.Interfaces.Repositories;

public interface IDepartmentsRepository
{
    Task Add(Department department, Address address);
    Task Delete(Guid id);
    Task<List<Department>> GetAll();
    Task Update(Department department, Address address);
    Task<Department> GetById(Guid id);
}