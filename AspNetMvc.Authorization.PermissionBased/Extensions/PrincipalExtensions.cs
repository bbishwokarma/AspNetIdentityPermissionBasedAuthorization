using AspNetMvc.Authorization.PermissionBased;
using System.Security.Principal;
using System.Linq;
using System.Collections.Generic;

public static class PrincipalExtensions
{
    public static bool HasPermission(this IPrincipal principal, string permission, string resourceType = null, string resourceId = null)
    {
        return HasPermissionIn(principal, new string[] { permission }, resourceType, resourceId);
    }
    
    public static bool HasPermissionIn(this IPrincipal principal, string[] permissions, string resourceType = null, string resourceId = null)
    {
        if (principal == null || principal.Identity == null)
        {
            return false;
        }
        var userId = principal.Identity.Name;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return false;
        }
        return PermissionAuthorizationService.HasPermissionIn(userId, permissions.ToList(), resourceType, resourceId);
    }
    

    public static List<string> GetPermissions(this IPrincipal principal)
    {
        var userId = principal.Identity.Name;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return new List<string>();
        }
        return PermissionAuthorizationService.GetPermissions(userId);
    }

    /// <summary>
    /// Required for removing cache of the user, if cache is enabled
    /// </summary>
    public static void PermissionServiceLogout(this IPrincipal principal)
    {
        var userId = principal.Identity.Name;
        PermissionAuthorizationService.Logout(userId);
    }
}
