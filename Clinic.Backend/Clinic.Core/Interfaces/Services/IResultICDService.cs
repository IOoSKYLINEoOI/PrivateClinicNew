using Clinic.Core.Models;
using CSharpFunctionalExtensions;

namespace Clinic.Application.Services
{
    public interface IResultICDService
    {
        Task<Result> AddResultICD(ResultICD resultICD);
        Task<Result> DeleteResultICD(Guid id);
        Task<Result<List<ResultICD>>> GetResultICD(Guid receptionId);
        Task<Result> UpdateResultICD(Guid id, string iCDCode, string? description, Guid receptionId);
    }
}