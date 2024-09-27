using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.DataAccess.Repositories;

public class EmployeesDepartmentsRepository : IEmployeesDepartmentsRepository
{
    private readonly ClinicDbContext _context;

    public EmployeesDepartmentsRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task Add(EmployeeDepartment employeeDepartment)
    {
        var position = await _context.Positions
            .FirstOrDefaultAsync(p => p.Id == employeeDepartment.PositionId)
            ?? throw new Exception($"Position with ID {employeeDepartment.PositionId} not found.");

        var employeeDepartmentEntity = new EmployeeDepartmentEntity()
        {
            EmployeeId = employeeDepartment.EmployeeId,
            DepartmentId = employeeDepartment.DepartmentId,
            Description = employeeDepartment.Description,
            PositionId = employeeDepartment.PositionId,
        };

        await _context.EmployeeDepartments.AddAsync(employeeDepartmentEntity);
        await _context.SaveChangesAsync();
    }


    public async Task Update(
        Guid employeeId,
        Guid departmentId,
        string? description,
        int positionId)
    {
        await _context.EmployeeDepartments
            .Where(x => x.EmployeeId == employeeId && x.DepartmentId == departmentId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.Description, description)
                .SetProperty(x => x.PositionId, positionId));
    }

    public async Task<EmployeeDepartment> GetById(Guid employeeId, Guid departmentId)
    {
        var employeeDepartmentEntity = await _context.EmployeeDepartments
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.EmployeeId == employeeId && u.DepartmentId == departmentId)
            ?? throw new Exception($"EmployeeDepartment with EmployeeId {employeeId} and DepartmentId {departmentId} not found.");

        var employeeDepartment = EmployeeDepartment.Create(
            employeeDepartmentEntity.EmployeeId,
            employeeDepartmentEntity.DepartmentId,
            employeeDepartmentEntity.Description,
            employeeDepartmentEntity.PositionId).Value;

        return employeeDepartment;
    }

    public async Task Delete(Guid employeeId, Guid departmentId)
    {
        await _context.EmployeeDepartments
            .Where(x => x.EmployeeId == employeeId && x.DepartmentId == departmentId)
            .ExecuteDeleteAsync();
    }
}