using Clinic.Core.Enums;
using Clinic.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

public class PolicyProvider
{
    private readonly RolePermissionsOptions _rolePermissionsOptions;

    public PolicyProvider(IOptions<RolePermissionsOptions> rolePermissionsOptions)
    {
        _rolePermissionsOptions = rolePermissionsOptions.Value ?? throw new ArgumentNullException(nameof(rolePermissionsOptions));
    }

    public void RegisterPolicies(AuthorizationOptions options)
    {
        if (_rolePermissionsOptions.RolePermissions == null || _rolePermissionsOptions.RolePermissions.Count == 0)
        {
            throw new InvalidOperationException("RolePermissionsOptions is not initialized.");
        }

        var permissionToRolesMap = new Dictionary<Permission, List<string>>();

        // Собираем роли для каждого разрешения
        foreach (var rolePermission in _rolePermissionsOptions.RolePermissions)
        {
            var role = rolePermission.Role;

            foreach (var permission in rolePermission.Permissions)
            {
                if (Enum.TryParse(permission, out Permission parsedPermission))
                {
                    if (!permissionToRolesMap.ContainsKey(parsedPermission))
                    {
                        permissionToRolesMap[parsedPermission] = new List<string>();
                    }

                    permissionToRolesMap[parsedPermission].Add(role);
                }
                else
                {
                    throw new ArgumentException($"Invalid permission: {permission}");
                }
            }
        }

        // Регистрируем политики на основе карты "разрешение -> роли"
        foreach (var kvp in permissionToRolesMap)
        {
            var permission = kvp.Key;
            var roles = kvp.Value;

            var policyName = permission.ToString();

            // Проверка, существует ли уже политика с таким именем
            if (options.GetPolicy(policyName) != null)
            {
                Console.WriteLine($"Policy already exists: {policyName}");
                continue;
            }

            // Добавление новой политики
            options.AddPolicy(policyName, policy =>
            {
                //policy.RequireRole(roles);
                policy.Requirements.Add(new PermissionRequirement(new[] { permission }));
            });

            Console.WriteLine($"Policy registered: {policyName} for roles: {string.Join(", ", roles)}");
        }
    }
}
