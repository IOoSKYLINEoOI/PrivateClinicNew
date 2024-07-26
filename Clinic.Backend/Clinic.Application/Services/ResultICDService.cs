using Clinic.Application.Services;
using Clinic.Core.Models;
using Clinic.DataAccess.Repositories;
using CSharpFunctionalExtensions;

public class ResultICDService : IResultICDService
{
    private readonly IResultsICDRepository _resultsICDRepository;

    public ResultICDService(IResultsICDRepository resultsICDRepository)
    {
        _resultsICDRepository = resultsICDRepository;
    }

    public async Task<Result> AddResultICD(ResultICD resultICD)
    {
        await _resultsICDRepository.Add(resultICD);
        return Result.Success();
    }

    public async Task<Result> DeleteResultICD(Guid id)
    {
        await _resultsICDRepository.Delete(id);
        return Result.Success();
    }

    public async Task<Result<List<ResultICD>>> GetResultICD(Guid receptionId)
    {
        var results = await _resultsICDRepository.Get(receptionId);
        return Result.Success(results);
    }

    public async Task<Result> UpdateResultICD(Guid id, string iCDCode, string? description, Guid receptionId)
    {
        await _resultsICDRepository.Update(id, iCDCode, description, receptionId);
        return Result.Success();
    }
}