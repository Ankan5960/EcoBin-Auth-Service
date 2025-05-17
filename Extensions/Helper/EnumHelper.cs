using Auth_Api.Model.Enums;

namespace User_Auth_API.Extensions.Helpers;

public static class EnumHelper
{
    private static readonly Dictionary<Roles, string> RoleNames = new()
    {
        { Roles.Admin, "Admin" },
        { Roles.User, "User" },
        { Roles.Collector, "Collector" },
        { Roles.Guest, "Guest" }
    };

    public static string GetRoleName(this Roles role)
    {
        return RoleNames.TryGetValue(role, out var name) ? name : string.Empty;
    }
}
