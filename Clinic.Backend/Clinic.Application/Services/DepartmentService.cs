using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using CSharpFunctionalExtensions;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentsRepository _departmentRepository;

    public DepartmentService(IDepartmentsRepository departmentsRepository)
    {
        _departmentRepository = departmentsRepository;
    }

    public async Task<Result> AddDepartment(Department department, Address address)
    {
        await _departmentRepository.Add(department, address);
        return Result.Success();
    }

    public async Task<Result<List<Department>>> GetAllDepartment()
    {
        var departments = await _departmentRepository.GetAll();
        return Result.Success(departments);
    }

    public async Task<Result<Department>> GetDepartmentById(Guid id)
    {
        var department = await _departmentRepository.GetById(id);
        if (department == null)
        {
            return Result.Failure<Department>("Department not found");
        }
        return Result.Success(department);
    }

    public async Task<Result> UpdateDepartment(Department department, Address address)
    {
        await _departmentRepository.Update(department, address);
        return Result.Success();
    }

    public async Task<Result> DeleteDepartment(Guid id)
    {
        await _departmentRepository.Delete(id);
        return Result.Success();
    }
}