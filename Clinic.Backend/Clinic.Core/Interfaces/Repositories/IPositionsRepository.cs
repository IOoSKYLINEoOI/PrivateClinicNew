using Clinic.Core.Models;

namespace Clinic.Core.Interfaces.Repositories;

public interface IPositionsRepository
{
    Task Add(Position position);
    Task Delete(Guid id);
    Task<List<Position>> GetAllPositions();
    Task Update(Guid id, string name, string? description);
}