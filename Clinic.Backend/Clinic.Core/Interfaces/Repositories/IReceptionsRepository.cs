using Clinic.Core.Models;

namespace Clinic.Core.Interfaces.Repositories;

public interface IReceptionsRepository
{
    Task Add(Reception reception);
    Task Delete(Guid id);
    Task<List<Reception>> GetAll(Guid userId);
    Task Update(Guid id, DateTime dateReceipt, DateTime? dateOfReturn, string? description, Guid userId, Guid deprtmentId, Guid employeeId);
}