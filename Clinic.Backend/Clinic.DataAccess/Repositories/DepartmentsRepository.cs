using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.DataAccess.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    private readonly ClinicDbContext _context;

    public DepartmentsRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task Add(Department department)
    {
        var address = await _context.Addresses
        .FirstOrDefaultAsync(a => a.Id == department.AddressId)
        ?? throw new Exception($"Address with ID {department.AddressId} not found.");

        var departmentEntity = new DepartmentEntity()
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
            AddressId = department.AddressId,
            Address = address
        };

        await _context.Departments.AddAsync(departmentEntity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(
        Guid id,
        string name,
        string? description,
        Guid addressId)
    {
        await _context.Departments
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.Name, name)
                .SetProperty(x => x.Description, description)
                .SetProperty(x => x.AddressId, addressId));
    }

    public async Task<List<Department>> GetAll()
    {
        var departmentEntities = await _context.Departments
            .AsNoTracking()
            .ToListAsync();

        var departments = departmentEntities
            .Select(de => Department.Create(de.Id, de.Name, de.Description, de.AddressId).Value)
            .ToList();

        return departments;
    }

    public async Task Delete(Guid id)
    {
        await _context.Departments
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
}