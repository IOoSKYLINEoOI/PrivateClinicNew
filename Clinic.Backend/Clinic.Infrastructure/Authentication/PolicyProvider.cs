using Clinic.Core.Enums;
using Clinic.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

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

        foreach (var rolePermission in _rolePermissionsOptions.RolePermissions)
        {
            var role = rolePermission.Role;

            foreach (var permission in rolePermission.Permissions)
            {
                if (Enum.TryParse(permission, out Permission parsedPermission))
                {
                    var policyName = parsedPermission.ToString();

                    // Проверка, существует ли уже политика с таким именем
                    if (options.GetPolicy(policyName) != null)
                    {
                        Console.WriteLine($"Policy already exists: {policyName}");
                        continue;
                    }

                    // Добавление новой политики
                    options.AddPolicy(policyName, policy =>
                    {
                        policy.RequireRole(role);
                        policy.Requirements.Add(new PermissionRequirement(new[] { parsedPermission }));
                    });

                    Console.WriteLine($"Policy registered: {policyName}");
                }
                else
                {
                    throw new ArgumentException($"Invalid permission: {permission}");
                }
            }
        }
    }
}
