
using Clinic.Core.Enums;
using CSharpFunctionalExtensions;

namespace Clinic.Core.Interfaces.Services;

public interface IUserService
{
    Task<Result<string>> Login(string email, string password);
    Task<Result> Register(string firstName, string lastName, string fatherName, DateOnly dateOfBirth, string email, string phoneNumber, string password);
    Task<Result> Update(
       Guid id,
       string firstName,
       string lastName,
       string fatherName,
       DateOnly dateOfBirth,
       Guid? addressId,
       Guid? imageId);
    Task<HashSet<Permission>> GetUserPermissions(Guid userId);
}