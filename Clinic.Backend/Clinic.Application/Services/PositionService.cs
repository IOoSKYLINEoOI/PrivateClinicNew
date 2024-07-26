using Clinic.Application.Services;
using Clinic.Core.Models;
using Clinic.DataAccess.Repositories;
using CSharpFunctionalExtensions;

public class PositionService : IPositionService
{
    private readonly IPositionsRepository _positionsRepository;

    public PositionService(IPositionsRepository positionsRepository)
    {
        _positionsRepository = positionsRepository;
    }

    public async Task<Result> AddPosition(Position position)
    {
        await _positionsRepository.Add(position);
        return Result.Success();
    }

    public async Task<Result> DeletePosition(Guid id)
    {
        await _positionsRepository.Delete(id);
        return Result.Success();
    }

    public async Task<Result<List<Position>>> GetAllPositions()
    {
        var positions = await _positionsRepository.GetAllPositions();
        return Result.Success(positions);
    }

    public async Task<Result> Update(Guid id, string name, string? description)
    {
        await _positionsRepository.Update(id, name, description);
        return Result.Success();
    }
}