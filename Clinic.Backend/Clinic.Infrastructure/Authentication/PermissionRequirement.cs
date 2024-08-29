using Clinic.Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Clinic.Infrastructure.Authentication;

public class PermissionRequirement : IAuthorizationRequirement
{
    public Permission[] Permissions { get; set; } = [];

    public PermissionRequirement(Permission[] permissions) => Permissions = permissions;
}
