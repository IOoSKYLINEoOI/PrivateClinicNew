using Clinic.Core.Models;
using CSharpFunctionalExtensions;
using System.Net;

namespace Clinic.Application.Services
{
    public interface IDepartmentService
    {
        Task<Result> AddDepartment(Department department, Address address);
        Task<Result> DeleteDepartment(Guid id);
        Task<Result<List<Department>>> GetAllDepartment();
        Task<Result> UpdateDepartment(Department department, Address address);
    }
}