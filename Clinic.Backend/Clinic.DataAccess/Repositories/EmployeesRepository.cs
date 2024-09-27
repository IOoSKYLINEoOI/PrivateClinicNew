using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.DataAccess.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly ClinicDbContext _context;

    public EmployeesRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task Add(Employee employee)
    {
        var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Id == employee.UserId)
        ?? throw new Exception($"User with ID {employee.UserId} not found.");

        var employeeEntity = new EmployeeEntity()
        {
            Id = employee.Id,
            HiringDate = employee.HiringDate,
            DateOfDismissal = employee.DateOfDismissal,
            UserId = employee.UserId,
            Description = employee.Description,
        };

        await _context.Employees.AddAsync(employeeEntity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(
        Guid id,
        DateOnly hiringDate,
        DateOnly? dateOfDismissal,
        string? description,
        Guid userId)
    {
        await _context.Employees
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.HiringDate, hiringDate)
                .SetProperty(x => x.DateOfDismissal, dateOfDismissal)
                .SetProperty(x => x.Description, description)
                .SetProperty(x => x.UserId, userId));
    }

    public async Task<List<Employee>> GetAll()
    {
        var employeeEntities = await _context.Employees
            .AsNoTracking()
            .ToListAsync();

        var employees = employeeEntities
            .Select(ee => Employee.Create(ee.Id, ee.HiringDate, ee.DateOfDismissal, ee.Description, ee.UserId).Value)
            .ToList();

        return employees;
    }

    public async Task Delete(Guid id)
    {
        await _context.Employees
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
}