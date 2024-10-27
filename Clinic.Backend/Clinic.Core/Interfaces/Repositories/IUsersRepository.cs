using Clinic.Core.Enums;
using Clinic.Core.Models;

namespace Clinic.Core.Interfaces.Repositories;

public interface IUsersRepository
{
    Task Add(User user);
    Task<User> GetByEmail(string email);
    Task<HashSet<Permission>> GetUserPermissions(Guid userId);
    Task Update(
         Guid id,
         string firstName,
         string lastName,
         string? fatherName,
         DateOnly dateOfBirth,
         Guid? addressId,
         Guid? imageId);
    Task<User> GetById(Guid id);
}