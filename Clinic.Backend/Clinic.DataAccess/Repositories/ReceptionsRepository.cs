using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.DataAccess.Repositories;

public class ReceptionsRepository : IReceptionsRepository
{
    private readonly ClinicDbContext _context;

    public ReceptionsRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task Add(Reception reception)
    {
        var receptionEntity = new ReceptionEntity()
        {
            Id = reception.Id,
            DateReceipt = reception.DateReceipt,
            DateOfReturn = reception.DateOfReturn,
            Description = reception.Description,
            UserId = reception.UserId,
            DepartmentId = reception.DepartmentId,
            EmployeeId = reception.EmployeeId,
        };

        await _context.Receptions.AddAsync(receptionEntity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(
        Guid id,
        DateTime dateReceipt,
        DateTime? dateOfReturn,
        string? description,
        Guid userId,
        Guid departmentId,
        Guid employeeId)
    {
        await _context.Receptions
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.DateReceipt, dateReceipt)
                .SetProperty(x => x.DateOfReturn, dateOfReturn)
                .SetProperty(x => x.Description, description)
                .SetProperty(x => x.UserId, userId)
                .SetProperty(x => x.DepartmentId, departmentId)
                .SetProperty(x => x.EmployeeId, employeeId));
    }

    public async Task<List<Reception>> GetAll(Guid userId)
    {
        var receptionEntities = await _context.Receptions
            .Where(x => x.UserId == userId)
            .AsNoTracking()
            .ToListAsync();

        var receptions = receptionEntities
            .Select(b => Reception.Create(b.Id, b.DateReceipt, b.DateOfReturn, b.Description, b.UserId, b.DepartmentId, b.EmployeeId).Value)
            .ToList();

        return receptions;
    }

    public async Task Delete(Guid id)
    {
        await _context.Receptions
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
}