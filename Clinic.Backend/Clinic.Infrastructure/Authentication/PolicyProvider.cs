using Clinic.Core.Enums;
using Clinic.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

public class PolicyProvider : IAuthorizationPolicyProvider
{
    private readonly DefaultAuthorizationPolicyProvider _fallbackPolicyProvider;
    private readonly RolePermissionsOptions _rolePermissionsOptions;
    private readonly ConcurrentDictionary<string, AuthorizationPolicy> _policiesCache = new();

    public PolicyProvider(IOptions<RolePermissionsOptions> rolePermissionsOptions, IOptions<AuthorizationOptions> options)
    {
        _fallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        _rolePermissionsOptions = rolePermissionsOptions.Value ?? throw new ArgumentNullException(nameof(rolePermissionsOptions));

        RegisterPolicies();
    }

    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        return Task.FromResult(_policiesCache.TryGetValue(policyName, out var policy) ? policy : null);
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        return _fallbackPolicyProvider.GetDefaultPolicyAsync();
    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        return _fallbackPolicyProvider.GetFallbackPolicyAsync();
    }

    private void RegisterPolicies()
    {
        if (_rolePermissionsOptions.RolePermissions == null || _rolePermissionsOptions.RolePermissions.Count == 0)
        {
            throw new InvalidOperationException("RolePermissionsOptions is not initialized.");
        }

        var permissionToRolesMap = new Dictionary<Permission, List<string>>();

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

        foreach (var kvp in permissionToRolesMap)
        {
            var permission = kvp.Key;
            var roles = kvp.Value;

            var policyName = permission.ToString();

            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new PermissionRequirement(new[] { permission }))
                .Build();

            _policiesCache.TryAdd(policyName, policy);

            Console.WriteLine($"Policy registered: {policyName} for roles: {string.Join(", ", roles)}");
        }
    }
}
