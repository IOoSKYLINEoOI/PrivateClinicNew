using Clinic.Core.Models;
using CSharpFunctionalExtensions;
using System;
using System.Threading.Tasks;

public interface IDepartmentService
{
    Task<Result<List<Department>>> GetAllDepartment();
    Task<Result<Department>> GetDepartmentById(Guid id); // Добавьте эту строку
    Task<Result> AddDepartment(Department department, Address address);
    Task<Result> UpdateDepartment(Department department, Address address);
    Task<Result> DeleteDepartment(Guid id);
}
