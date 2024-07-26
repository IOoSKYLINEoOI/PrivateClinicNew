using Clinic.Core.Models;
using CSharpFunctionalExtensions;

namespace Clinic.Application.Services
{
    public interface IPositionService
    {
        Task<Result> AddPosition(Position position);
        Task<Result> DeletePosition(Guid id);
        Task<Result<List<Position>>> GetAllPositions();
        Task<Result> Update(Guid id, string name, string? description);
    }
}