using Clinic.Core.Enums;
using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Interfaces.Services;

namespace Clinic.Application.Services;

public class PermissionService : IPermissionService
{
    private readonly IUsersRepository _usersRepository;

    public PermissionService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Task<HashSet<Permission>> GetPermissionsAsync(Guid userId)
    {
        return _usersRepository.GetUserPermissions(userId);
    }
}
