using Clinic.Application.Services;
using Clinic.Core.Models;
using Clinic.DataAccess.Repositories;
using CSharpFunctionalExtensions;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentsRepository _departmentRepository;

    public DepartmentService(IDepartmentsRepository departmentsRepository)
    {
        _departmentRepository = departmentsRepository;
    }

    public async Task<Result> AddDepartment(Department department)
    {
        await _departmentRepository.Add(department);
        return Result.Success();
    }

    public async Task<Result<List<Department>>> GetAllDepartment()
    {
        var departments = await _departmentRepository.GetAll();
        return Result.Success(departments);
    }

    public async Task<Result> UpdateDepartment(
        Guid id,
        string name,
        string? description,
        Guid addressId)
    {
        await _departmentRepository.Update(id, name, description, addressId);
        return Result.Success();
    }

    public async Task<Result> DeleteDepartment(Guid id)
    {
        await _departmentRepository.Delete(id);
        return Result.Success();
    }
}