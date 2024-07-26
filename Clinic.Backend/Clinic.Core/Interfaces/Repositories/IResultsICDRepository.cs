using Clinic.Core.Models;

namespace Clinic.DataAccess.Repositories
{
    public interface IResultsICDRepository
    {
        Task Add(ResultICD resultICD);
        Task Delete(Guid id);
        Task<List<ResultICD>> Get(Guid receptionId);
        Task Update(Guid id, string iCDCode, string? description, Guid receptionId);
    }
}