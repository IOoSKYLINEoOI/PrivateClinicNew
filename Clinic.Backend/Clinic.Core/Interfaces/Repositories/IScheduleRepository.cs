using Clinic.Core.Models;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.Repositories;

public interface IScheduleRepository
{
    Task<Result> Add(Schedule schedule);
    Task<Result<Schedule>> GetById(Guid id);
    Task<List<Schedule>> GetAll();
    Task<Result> Update(Schedule schedule);
    Task<Result> Delete(Guid id);
}
