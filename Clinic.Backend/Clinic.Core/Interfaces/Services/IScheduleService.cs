using Clinic.Core.Models;
using CSharpFunctionalExtensions;

namespace Clinic.Core.Interfaces.Services;

public interface IScheduleService
{
    Task<Result> AddSchedule(Schedule schedule);
    Task<Result<Schedule>> GetByIdSchedule(Guid id);
    Task<Result<List<Schedule>>> GetAllSchedules();
    Task<Result> UpdateSchedule(Schedule schedule);
    Task<Result> DeleteSchedule(Guid id);
}
