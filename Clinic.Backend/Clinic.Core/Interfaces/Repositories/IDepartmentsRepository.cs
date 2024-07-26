﻿using Clinic.Core.Models;

namespace Clinic.DataAccess.Repositories
{
    public interface IDepartmentsRepository
    {
        Task Add(Department department);
        Task Delete(Guid id);
        Task<List<Department>> GetAll();
        Task Update(Guid id, string name, string? description, Guid addressId);
    }
}