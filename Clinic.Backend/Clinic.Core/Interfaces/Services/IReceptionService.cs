using Clinic.Core.Models;
using CSharpFunctionalExtensions;

namespace Clinic.Application.Services
{
    public interface IReceptionService
    {
        Task<Result> AddReception(Reception reception);
        Task<Result> DeleteReception(Guid id);
        Task<Result<List<Reception>>> GetAllReceptionUser(Guid userId);
        Task<Result> UpdateReception(Guid id, DateTime dateReceipt, DateTime? dateOfReturn, string? description, Guid userId, Guid deprtmentId, Guid employeeId);
    }
}