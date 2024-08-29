public class RolePermissionsOptions
{
    public List<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();  // Инициализируем список по умолчанию
}

public class RolePermission
{
    public string Role { get; set; }
    public List<string> Permissions { get; set; } = new List<string>();  // Инициализируем список по умолчанию
}

