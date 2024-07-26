using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.DataAccess.Repositories;

public class ResultsICDRepository : IResultsICDRepository
{
    private readonly ClinicDbContext _context;

    public ResultsICDRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task Add(ResultICD resultICD)
    {
        var resultICDEntity = new ResultICDEntity()
        {
            Id = resultICD.Id,
            ICDCode = resultICD.ICDCode,
            Description = resultICD.Description,
            ReceptionId = resultICD.ReceptionId,
        };

        await _context.ResultsICD.AddAsync(resultICDEntity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(
        Guid id,
        string iCDCode,
        string? description,
        Guid receptionId)
    {
        await _context.ResultsICD
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.ICDCode, iCDCode)
                .SetProperty(x => x.ReceptionId, receptionId)
                .SetProperty(x => x.Description, description));
    }

    public async Task<List<ResultICD>> Get(Guid receptionId)
    {
        var resultICDEntities = await _context.ResultsICD
            .Where(x => x.ReceptionId == receptionId)
            .AsNoTracking()
            .ToListAsync();

        var resultsICD = resultICDEntities
            .Select(b => ResultICD.Create(b.Id, b.ICDCode, b.Description, b.ReceptionId).Value)
            .ToList();

        return resultsICD;
    }

    public async Task Delete(Guid id)
    {
        await _context.ResultsICD
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
}