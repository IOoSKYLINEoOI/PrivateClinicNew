using Clinic.Application.Services;
using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using CSharpFunctionalExtensions;

public class ReceptionService : IReceptionService
{
    private readonly IReceptionsRepository _receptionsRepository;

    public ReceptionService(IReceptionsRepository receptionsRepository)
    {
        _receptionsRepository = receptionsRepository;
    }

    public async Task<Result> AddReception(Reception reception)
    {
        await _receptionsRepository.Add(reception);
        return Result.Success();
    }

    public async Task<Result> DeleteReception(Guid id)
    {
        await _receptionsRepository.Delete(id);
        return Result.Success();
    }

    public async Task<Result<List<Reception>>> GetAllReceptionUser(Guid userId)
    {
        var receptions = await _receptionsRepository.GetAll(userId);
        return Result.Success(receptions);
    }

    public async Task<Result> UpdateReception(Guid id, DateTime dateReceipt, DateTime? dateOfReturn, string? description, Guid userId, Guid deprtmentId, Guid employeeId)
    {
        await _receptionsRepository.Update(id, dateReceipt, dateOfReturn, description, userId, deprtmentId, employeeId);
        return Result.Success();
    }
}