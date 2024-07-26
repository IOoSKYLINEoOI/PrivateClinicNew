using Clinic.Core.Models;
using CSharpFunctionalExtensions;

namespace Clinic.Application.Services
{
    public interface IDepartmentService
    {
        Task<Result> AddDepartment(Department department);
        Task<Result> DeleteDepartment(Guid id);
        Task<Result<List<Department>>> GetAllDepartment();
        Task<Result> UpdateDepartment(Guid id, string name, string? description, Guid addressId);
    }
}