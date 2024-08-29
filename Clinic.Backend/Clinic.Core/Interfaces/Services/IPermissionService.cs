using Clinic.Core.Enums;

namespace Clinic.Core.Interfaces.Services;

public interface IPermissionService
{
    Task<HashSet<Permission>> GetPermissionsAsync(Guid userId);
}