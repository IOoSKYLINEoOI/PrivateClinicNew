using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.DataAccess.Repositories;

public class PositionsRepository : IPositionsRepository
{
    private readonly ClinicDbContext _context;

    public PositionsRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task Add(Position position)
    {
        var positionEntity = new PositionEntity()
        {
            Id = position.Id,
            Name = position.Name,
            Description = position.Description,
        };

        await _context.Positions.AddAsync(positionEntity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(
        int id,
        string name,
        string? description)
    {
        await _context.Positions
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.Name, name)
                .SetProperty(x => x.Description, description));
    }

    public async Task<List<Position>> GetAllPositions()
    {
        var positionEntities = await _context.Positions
            .AsNoTracking()
            .ToListAsync();

        var positions = positionEntities
            .Select(b => Position.Create(b.Id, b.Name, b.Description).Value)
            .ToList();

        return positions;
    }

    public async Task Delete(int id)
    {
        await _context.Positions
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
}